using DAL.Model.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model
{
    public class UnitOfWork
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private GeneralRepository <Tbl_User> _UserRepository { get; set; }
        public GeneralRepository<Tbl_User> UserRepository { get
            {
                if (_UserRepository == null)
                {
                    _UserRepository = new GeneralRepository<Tbl_User>(db);
                }
                return _UserRepository;
            }
        }
        private GeneralRepository<Tbl_ApiSetting> _ApiRepository { get; set; }
        public GeneralRepository<Tbl_ApiSetting> ApiRepository
        {
            get
            {
                if (_ApiRepository == null)
                {
                    _ApiRepository = new GeneralRepository<Tbl_ApiSetting>(db);
                }
                return _ApiRepository;
            }
        }

    }
}
