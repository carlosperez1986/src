using System;
using Microsoft.Extensions.Configuration;
using Modular.Modules.Core.Services;


public class Test : ITest
{
    public IConfiguration _config;

    public Test(IConfiguration config)
    {
        _config = config;
    }

    public string TestDL()
    {
        return _config.GetConnectionString("DefaultConnection");
    }

}