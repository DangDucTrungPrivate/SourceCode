using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using GCMP.Models.Entities;
using GCMP.Models.Helper;
using GCMP.ViewModels.AccountViewModel;

namespace GCMP.Models.AccountModel
{
    public class AccountModel
    {
        private readonly GCMPDBEntities _db = new GCMPDBEntities();

        public AccountModel()
        {
        }

        public Boolean RegisterAccount(AccountRegisterViewModel regModel)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int sizesalt = rand.Next(1, 100);

            String salt = AccountHelper.CreateSalt(sizesalt);
            String passAndSalt = regModel.Password + salt;
            String hassedPass = AccountHelper.HashPassword(passAndSalt);

            var singleOrDefault = _db.Roles.SingleOrDefault(r => r.RoleName == Helper.Const.User);
            if (singleOrDefault != null)
            {
                var user = new User
                {
                    UserName = regModel.Username,
                    Password = hassedPass,
                    KeyValue = salt,
                    IsActive = false,
                    RoleId = singleOrDefault.Id,
                    Status = Const.UActive,
                    Description = ""
                };


                try
                {
                    _db.Users.Add(user);
                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                var userinfo = new UserInfo
                {
                    IdCard = regModel.IdNumber,
                    Address = regModel.Address,
                    Phone = regModel.Phonenumber,
                    Gender = regModel.Male,
                    DayOfBirth = regModel.DateOfBirth,
                    LastLogin = DateTime.Now,
                    Description = "",
                    User = _db.Users.Find(user.Id)
                };

                var userconfig = new UserConfig
                {
                    DisplayNickname = false,
                    AllowToSellCard = false,
                    User = _db.Users.Find(user.Id)
                };

                try
                {
                    _db.UserInfoes.Add(userinfo);
                    _db.SaveChanges();
                    _db.UserConfigs.Add(userconfig);
                    _db.SaveChanges();
                    return true;
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                    return false;
                }
            }
            return false;
        }

        public Boolean CheckExistedUser(string username)
        {
            if (_db.Users.Any(u => u.UserName == username))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Boolean CheckExitstedIdCard(string idNumber)
        {
            if (_db.UserInfoes.Any(u => u.IdCard == idNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean CheckExistEmail(string email)
        {
            if (_db.Users.Any(u => u.UserName == email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean Login(string username, string password)
        {
            var user = _db.Users.SingleOrDefault(u => u.UserName == username && u.Status.Equals(Const.UActive));

            if (user != null)
            {
                String salt = user.KeyValue;
                String passAndSalt = password + salt;
                String hassedPass = AccountHelper.HashPassword(passAndSalt);
                if (hassedPass.Equals(user.Password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
 
}