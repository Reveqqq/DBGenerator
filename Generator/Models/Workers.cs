using System;

namespace Generator.Models
{
    internal class Workers
    {
        public Guid id;
        public string fullname;
        public Guid position_id;
        public string login;
        public string salt;
        public string hash_password;
    }
}