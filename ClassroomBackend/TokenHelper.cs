using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ClassroomBackend.Models;
using System.Web.Http;
using System.Net.Http;


namespace ClassroomBackend
{
    public class TokenHelper
    {
        public static User Authorize(HttpRequestMessage Request)
        {
            try
            {

                var auth = Request.Headers.Authorization;
                if (auth == null || auth.Scheme != "Bearer") return null;
                var token = auth.Parameter;
                var th = new TokenHelper();
                var payload = th.DecodeToken(token);

                return new User(payload);
            }
            catch
            {
                // log token error?
            }
            return null;
        }

        public string CreateToken(JwtPayload payload)
        {
            // TODO: change (and move) for production
            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256Signature);

            var header = new JwtHeader(signingCredentials);
            var secToken = new JwtSecurityToken(header, payload);

            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(secToken);

            return tokenString;
        }
        public string ReadToken(string token)
        {
            string tokenStr;
            var checker = new JwtSecurityTokenHandler();
            try
            {
                var tkn = checker.ReadToken(token);
                tokenStr = tkn.ToString();
            }
            catch
            {
                return null;
            }

            return tokenStr;

        }
        public JwtPayload DecodeToken(string token)
        {
            var ts = ReadToken(token);
            if (ts == null) return null;
            var payload = JsonExtensions.DeserializeJwtPayload(ts.Substring(ts.LastIndexOf('.') + 1));
            return payload;
        }

        public object FromPayload(JwtPayload payload, string key)
        {
            object val;
            if (payload == null) return null;
            if (!payload.TryGetValue(key, out val)) return null;
            return val;
        }
    }
}