using System;
using Microsoft.EntityFrameworkCore;
using Modular.Core.Data;
using Modular.Modules.Core.Models;

namespace Modular.Modules.EmailSMTP.Data
{
    public class EmailConfigCustomModelBuilder : ICustomModelBuilder
    {

        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppSetting>().HasData(
                new AppSetting("SmtpServer") { Module = "EmailSenderSmpt", IsVisibleInCommonSettingPage = false, Value = "smtp.gmail.com" },
                new AppSetting("SmtpPort") { Module = "EmailSenderSmpt", IsVisibleInCommonSettingPage = false, Value = "587" },
                new AppSetting("SmtpUsername") { Module = "EmailSenderSmpt", IsVisibleInCommonSettingPage = false, Value = "" },
                new AppSetting("SmtpPassword") { Module = "EmailSenderSmpt", IsVisibleInCommonSettingPage = false, Value = "" }
                );
        }
    }
}
