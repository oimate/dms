﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmspl.common.BigEndianExtension
{
    public static class BigEndian
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static short ToBigEndian(this short value)
        {
            return System.Net.IPAddress.HostToNetworkOrder(value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToBigEndian(this int value)
        {
            return System.Net.IPAddress.HostToNetworkOrder(value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ToBigEndian(this long value)
        {
            return System.Net.IPAddress.HostToNetworkOrder(value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static short FromBigEndian(this short value)
        {
            return System.Net.IPAddress.NetworkToHostOrder(value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int FromBigEndian(this int value)
        {
            return System.Net.IPAddress.NetworkToHostOrder(value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long FromBigEndian(this long value)
        {
            return System.Net.IPAddress.NetworkToHostOrder(value);
        }
    }
}
