using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TCP
{
    public delegate void ReceiveDataDelegate(Socket socket, byte[] data);
    public class TCP_MasterSlave
    {
        #region Public Properties

        public string Name
        { get; set; }

        public ServerType Type
        { get; set; }

        public ReceiveDataDelegate ReceiveData
        { get; set; }
        public EventHandler Disconected { get; set; }

        public string IP
        { get; set; }
        public Int16 Port
        { get; set; }

        public ConnStat Status
        { get; private set; }
        public ErrorCode Error
        { get; private set; }
        public string ErrorMessage
        { get; private set; }

        private static object serverLock = new object();
        private static List<ConnectionInfo> connections = new List<ConnectionInfo>();
        private static List<ConnectionInfo> socketList = new List<ConnectionInfo>();

        private Socket socket;

        #endregion
        #region Constructor
        public TCP_MasterSlave(ServerType type, string ip, Int16 slot, string name)
        {
            Type = type;
            IP = ip;
            Port = slot;
            Name = name;
        }
        public TCP_MasterSlave()
            : this(ServerType.Server, "127.0.0.1", 9000, "Server")
        { }
        #endregion
        #region Connection (Open, Close)
        public void Open()
        {
            IPAddress ipAdress;
            ErrorMessage = "";
            if (string.IsNullOrWhiteSpace(Name))//login validation
            {
                Error = ErrorCode.NoName;
            }
            else if (!IPAddress.TryParse(IP, out ipAdress))//ip validation
            {
                Error = ErrorCode.WrongConfig;
                return;
            }
            else//connection to server
            {
                if (Port == 0)
                {
                    Error = ErrorCode.WrongSlot;
                    return;
                }

                // Create the socket, bind it, and start listening
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    switch (Type)
                    {
                        case ServerType.Server:
                            Console.Write("Starting TCP server... ");
                            socket.Blocking = false;
                            IPEndPoint myEndpoint = new IPEndPoint(IPAddress.Any, Port);
                            socket.Bind(myEndpoint);
                            socket.Listen((int)SocketOptionName.MaxConnections);
                            socket.BeginAccept(new AsyncCallback(AcceptCallback), socket);
                            for (int i = 0; i < 2; i++)
                                socket.BeginAccept(new AsyncCallback(AcceptCallback), socket);
                            Status = ConnStat.Open;
                            Console.WriteLine("Done. Listening.");
                            break;
                        case ServerType.Client:
                            ConnectionInfo info = new ConnectionInfo();
                            info.Socket = socket;
                            IPEndPoint ipEndPoint = new IPEndPoint(ipAdress, Port);
                            lock (socketList)
                            {
                                socketList.Add(info);
                            }
                            Console.Write("Client Connecting... ");
                            try
                            {
                                info.Socket.Connect(ipEndPoint);
                                Console.WriteLine("Client conn Done.");
                                info.Socket.BeginReceive(info.Buffer, 0, info.Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), info);
                                Status = ConnStat.Connected;
                            }
                            catch (Exception e)
                            {
                                lock (socketList)
                                {
                                    socketList.Remove(info);
                                }
                                Console.WriteLine("Fail.");
                                Console.WriteLine(e);
                            }

                            //  socket.Connect(ipEndPoint);
                            break;
                        default:
                            Error = ErrorCode.WrongConfig;
                            break;
                    }
                }
                catch (SocketException ex)
                {
                    ErrorMessage = ex.Message;
                    Error = ErrorCode.SocketException;
                    Console.WriteLine("Fail.");
                    Console.WriteLine(ex);
                    return;
                }
                Error = ErrorCode.NoError;
            }
        }
        private void AcceptCallback(IAsyncResult result)
        {
            Console.WriteLine("Accept!");
            ConnectionInfo connection = new ConnectionInfo();
            try
            {
                // Finish Accept
                Socket s = (Socket)result.AsyncState;
                connection.Socket = s.EndAccept(result);
                connection.Socket.Blocking = false;
                connection.Buffer = new byte[2024];
                lock (connections) connections.Add(connection);

                Console.WriteLine("New connection from " + s);

                // Start Receive
                connection.Socket.BeginReceive(connection.Buffer, 0,
                    connection.Buffer.Length, SocketFlags.None,
                    new AsyncCallback(ReceiveCallback), connection);
                // Start new Accept
                socket.BeginAccept(new AsyncCallback(AcceptCallback),
                    result.AsyncState);
            }
            catch (SocketException exc)
            {
                CloseConnection(connection);
                Console.WriteLine("Socket exception: " + exc.SocketErrorCode);
            }
            catch (Exception exc)
            {
                CloseConnection(connection);
                Console.WriteLine("Exception: " + exc);
            }
        }
        private void ReceiveCallback(IAsyncResult result)
        {
            ConnectionInfo connection = (ConnectionInfo)result.AsyncState;
            try
            {
                int bytesRead = connection.Socket.EndReceive(result);
                if (0 != bytesRead)
                {
                    lock (serverLock)
                    {
                        //  byte[] = new tabela ;
                        //  string text = GetString(connection.Buffer);
                        string text = Encoding.UTF8.GetString(connection.Buffer, 0, bytesRead);
                        Console.WriteLine(text);
                        OnDataReceive(connection.Socket, connection.Buffer);
                    }
                    lock (connections)
                    {
                        foreach (ConnectionInfo conn in connections)
                        {
                            if (connection != conn)
                            {
                                conn.Socket.Send(connection.Buffer, bytesRead,
                                    SocketFlags.None);
                            }
                        }
                    }
                    connection.Socket.BeginReceive(connection.Buffer, 0,
                        connection.Buffer.Length, SocketFlags.None,
                        new AsyncCallback(ReceiveCallback), connection);
                }
                else CloseConnection(connection);
            }
            catch (SocketException)
            {
                CloseConnection(connection);
            }
            catch (Exception)
            {
                CloseConnection(connection);
            }
        }

        private void OnDataReceive(Socket socket, byte[] data)
        {
            if (ReceiveData != null)
            {
                ReceiveData(socket, data);
            }
        }
        private void CloseConnection(ConnectionInfo ci)
        {
            System.Diagnostics.Debug.WriteLine("Closing Connection...");
            if (ci == null) return;
            if (ci.Socket == null) return;
            ci.Socket.Close();
            lock (connections) connections.Remove(ci);
            System.Diagnostics.Debug.WriteLine("Connection Closed");
        }

        public void Close()
        {
            Status = ConnStat.Disconnected;
            switch (Type)
            {
                case ServerType.Server:
                    Console.Write("Shutting down server... ");
                    try
                    {
                        socket.Close();
                    }
                    catch { }
                    return;
                case ServerType.Client:
                    Console.Write("Shutting down client... ");
                    try
                    {
                        lock (socketList)
                        {
                            for (int i = socketList.Count - 1; i >= 0; i--)
                            {
                                try
                                {
                                    socketList[i].Socket.Close();
                                }
                                catch { }
                                socketList.RemoveAt(i);
                            }
                        }
                    }
                    catch { }
                    Thread.Sleep(500);
                    Console.WriteLine("Bye.");
                    return;
                default:
                    return;
            }

        }

        #endregion
        #region Data (Send, Receive)
        public void SendToServer(byte[] data)
        {
            switch (Type)
            {
                case ServerType.Server:
                    break;
                case ServerType.Client:
                    if (Status == ConnStat.Connected)
                    {
                        socket.Send(data, SocketFlags.None);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Type");
            }
        }
        public void SendTo(Socket socket, byte[] data)
        {
            switch (Type)
            {
                case ServerType.Server:
                    if (Status == ConnStat.Open)
                    {
                        socket.Send(data, SocketFlags.None);
                    }
                    break;
                case ServerType.Client:
                    if (Status == ConnStat.Connected)
                    {
                        socket.Send(data, SocketFlags.None);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Type");
            }
        }
        public void SendToAll(string Name, string Data)
        {
            lock (connections)
            {
                switch (Type)
                {
                    case ServerType.Server:
                        if (Status == ConnStat.Open)
                        {
                            foreach (ConnectionInfo conn in connections)
                            {
                                byte[] bytes = Encoding.UTF8.GetBytes(Data + "\n");
                                conn.Socket.Send(bytes, bytes.Length,
                                    SocketFlags.None);
                            }
                        }
                        break;
                    case ServerType.Client:
                        if (Status == ConnStat.Connected)
                        {
                            byte[] bytes = Encoding.UTF8.GetBytes(Data + "\n");
                            socket.Send(bytes, bytes.Length,
                                SocketFlags.None);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
        #region Utilities
        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        //private void ConnectionToServerLost()
        //{
        //    socket.Close();
        //    thread.Abort();
        //}
        #endregion
        #region Classes
        private class ConnectionInfo
        {
            private Socket socket;
            private byte[] buffer = new byte[2048];

            public Socket Socket { get { return socket; } set { socket = value; } }
            public byte[] Buffer { get { return buffer; } set { buffer = value; } }
        }
        #endregion
    }
    #region Enums
    public enum ServerType
    {
        Server = 1,
        Client = 2,
    }
    public enum ConnStat
    {
        Unknow,
        Open,
        Close,
        Connected,
        Disconnected,
        Busy,
    }
    public enum ErrorCode
    {
        NoError,
        WrongConfig,
        IPAdressNotAvailable,
        NoName,
        WrongSlot,
        WrongPartnerIP,
        SocketException,
        SendData,
        ReveiveData,
    }
    #endregion
}
