using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WebAPI.Sql
{
    public abstract class ActionOnDB
    {
        protected abstract void Log(string msg);
    }
}