using System;
using AkademetreMVC.Models;

namespace AkademetreMVC.Data
{
	public interface IUserService 
	{
        public IList<User> GetUsers();
        public User Create(User user);

        byte[] GenerateExcel();
    }
}

