 /**////////////////////////////////////////////////////////////////////////////////////////
 // Description: BLL class .
 // ---------------------
 // Copyright  2009 hammergo@163.com
 // ---------------------
 // History
 //    2013年2月2日 20:06:23    zfking    
 /**////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Collections.Generic;
using hammergo.Model;
using hammergo.DALFactory;
using hammergo.IDAL;
using System.Security.Cryptography;


namespace hammergo.BLL
{
    /// <summary>
    /// 业务逻辑类的摘要说明。
    /// </summary>

    public class SysUserBLL : SysUserBLLBase
    {

        /// <summary>
        /// 产生随机字符串
        /// </summary>
        /// <param name="size">字符数组大小</param>
        /// <returns></returns>
        public string CreateSalt(int size)
        {
            // Generate a cryptographic random number using the cryptographic
            // service provider
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);

        }

        /// <summary>
        /// 产生随机字符串,字符串长度为5
        /// </summary>
        /// <returns></returns>
        public string CreateSalt()
        {
            return CreateSalt(5);
        }

        /// <summary>
        /// 检查用户提交的用户名和密码是否和数据库里存储的一致
        /// </summary>
        /// <param name="suppliedPassword"></param>
        /// <param name="dbPasswordHash"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public bool VerifyPassword(string suppliedPassword, string dbPasswordHash, string salt)
        {

            bool passwordMatch = false;

            try
            {
                string hashedPasswordAndSalt = CreatePasswordHash(suppliedPassword, salt);
                // Now verify them.
                passwordMatch = hashedPasswordAndSalt.Equals(dbPasswordHash);
            }
            catch (Exception e)
            {
                throw new Exception("exception at VerifyPassword method in ther UserData class." + e.ToString());
            }

            return passwordMatch;
        }

        /// <summary>
        /// 将密码和随机生成的salt连起来,用"SHA1"算法取得其散列值
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd =
                System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(
                saltAndPwd, "SHA1");
            return hashedPwd;
        }


        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool checkPassword(string userName, string password)
        {

            SysUser user = GetModelBy_UserName(userName);


            if (user == null)
            {
                //用户不存在
                return false;
            }


            string dbPasswordHash = user.PasswordHash;//获取数据库中的密码散列
            string salt = user.Salt;//获取随机字符串

            bool isAuthenticated = VerifyPassword(password, dbPasswordHash, salt);
            //判断密码是否正确

            return isAuthenticated;
        }




    }
}



