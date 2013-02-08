﻿namespace App.FileIO
{
    public static class BaseCom
    {
        /// <summary>
        /// List -> IsEmptyList
        /// </summary>
        /// <param name="iList"></param>
        /// <returns></returns>
        public static bool IsEmptyList(this System.Collections.Generic.List<string> iList)
        {
            #region List -> IsEmptyList

            return iList == null || iList.Count <= 0;

            #endregion
        }

        /// <summary>
        /// String[] -> IsEmptyStrings
        /// </summary>
        /// <param name="iString"></param>
        /// <returns></returns>
        public static bool IsEmptyStrings(this string[] iString)
        {
            #region String[] -> IsEmptyStrings

            return iString == null || iString.Length <= 0;

            #endregion
        }

        /// <summary>
        /// Byte[] -> IsEmptyBytes
        /// </summary>
        /// <param name="iBytes"></param>
        /// <returns></returns>
        public static bool IsEmptyBytes(this byte[] iBytes)
        {
            #region Byte[] -> IsEmptyBytes

            return iBytes == null || iBytes.Length <= 0;

            #endregion
        }


        /// <summary>
        /// object -> IsEmpty
        /// </summary>
        /// <param name="iO"></param>
        /// <returns></returns>
        public static bool IsEmpty(this object iO)
        {
            #region object -> IsEmpty

            return iO == null;

            #endregion
        }

        /// <summary>
        /// Thread -> IsLive
        /// </summary>
        /// <param name="iThread"></param>
        /// <returns></returns>
        public static bool IsLive(this System.Threading.Thread iThread)
        {
            #region Thread -> IsLive

            return iThread != null && iThread.IsAlive;

            #endregion
        }
    }
}