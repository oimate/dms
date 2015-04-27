using dmspl.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmspl.datastorage.usermanager
{
    //class UserManager
    //{
    //    DmsDataset dmsDataset;

    //    User currentuser;
    //    internal User Currentuser
    //    {
    //        get { return currentuser; }
    //        set { currentuser = value; }
    //    }

    //    public UserManager(DmsDataset dmsDataset)
    //    {
    //        this.dmsDataset = dmsDataset;
    //        this.sec_DataAdapter = new DmsDatasetTableAdapters.DMS_SECTableAdapter();
    //        sec_DataAdapter.Fill(dmsDataset.DMS_SEC);
    //        this.usr_DataAdapter = new DmsDatasetTableAdapters.DMS_USRTableAdapter();
    //        usr_DataAdapter.Fill(dmsDataset.DMS_USR);
    //    }

    //    public bool Login(string login, string password)
    //    {
    //        string hpwd = Obfuscation.Code(login, password);
    //        long? id = usr_DataAdapter.GetId(login, hpwd);

    //        if (id != null)
    //        {
    //            DmsDataset.DMS_USRRow userrow = dmsDataset.DMS_USR.FindById(id.Value);
    //            int secuid = userrow.security;

    //            DmsDataset.DMS_SECRow securow = dmsDataset.DMS_SEC.FindById(secuid);
    //            bool canAdd = securow.canAdd;
    //            bool canModify = securow.canModify;
    //            bool canRead = securow.canRead;

    //            Secu secu = new Secu
    //            {
    //                Id = secuid,
    //                CanAdd = canAdd,
    //                CanModify = canModify,
    //                CanRead = canRead
    //            };

    //            userrow.lastlogin = DateTime.Now;
    //            usr_DataAdapter.Update(userrow);

    //            currentuser = new User
    //            {
    //                Id = id.Value,
    //                Login = userrow.login,
    //                Name = userrow.name,
    //                Surname = userrow.surname,
    //                Pwd = userrow.pwd,
    //                Lastlogin = userrow.lastlogin,
    //                Security = secu
    //            };

    //            return true;
    //        }

    //        return false;
    //    }
    //}

    class User
    {
        long id;
        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        string login;
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        string surname;
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        string pwd;
        public string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }

        DateTime lastlogin;
        public DateTime Lastlogin
        {
            get { return lastlogin; }
            set { lastlogin = value; }
        }

        Secu security;
        internal Secu Security
        {
            get { return security; }
            set { security = value; }
        }
    }

    class Secu
    {
        int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        bool canRead;

        public bool CanRead
        {
            get { return canRead; }
            set { canRead = value; }
        }
        bool canModify;

        public bool CanModify
        {
            get { return canModify; }
            set { canModify = value; }
        }
        bool canAdd;

        public bool CanAdd
        {
            get { return canAdd; }
            set { canAdd = value; }
        }
    }
}
