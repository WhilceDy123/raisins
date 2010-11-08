﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Services;
using System.Web.Mvc;

namespace Raisins.Client.Web.Models
{
    public class AccountModel
    {
        public float Amount { get; set; }
        public long BeneficiaryID { get; set; }
        public string Currency { get; set; }
        public string Email { get; set; }
        public long ID { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string SelectedBeneficiary { get; set; }
        public IEnumerable<SelectListItem> BeneficiaryOptions 
        {
            get
            {
                Beneficiary[] beneficiaries = Beneficiary.FindAll();
                List<SelectListItem> optionList = new List<SelectListItem>();

                foreach(Beneficiary b in beneficiaries)
                {
                    optionList.Add(new SelectListItem { Value = b.ID.ToString(), Text = b.Name });
                }
                return optionList.ToArray();
            }
        }
        
        public static AccountModel[] FindAll()
        {            
            Account[] accounts = Account.FindAll();
            List<AccountModel> models = new List<AccountModel>();

            foreach (Account account in accounts)
            {
                models.Add(ToModel(account));
            }

            return models.ToArray();
        }

        public static void Save(AccountModel model)
        {
            model.BeneficiaryID = 1;

            Account data = ToData(model);
            
            data.Save();
        }

        protected static AccountModel ToModel(Account data)
        {
            AccountModel model = new AccountModel();
            model.Amount = data.Amount;
            model.Currency = data.Currency;
            model.Email = data.Email;
            model.ID = data.ID;
            model.Location = data.Location;
            model.Name = data.Name;
            
            return model;
        }

        protected static Account ToData(AccountModel model)
        {
            Account data = new Account();
            data.Amount = model.Amount;
            data.Currency = model.Currency;
            data.Email = model.Email;
            data.ID = model.ID;
            data.Location = model.Location;
            data.Name = model.Name;

            data.Beneficiary = Beneficiary.Find(model.BeneficiaryID);

            return data;
        }
    }
}