﻿using System;
namespace Modular.Modules.EmailSMTP
{
    public class EmailConfig
    {
        public string SmtpServer { get; set; }

        public int SmtpPort { get; set; }

        public string SmtpUsername { get; set; }

        public string SmtpPassword { get; set; }
    }
}
