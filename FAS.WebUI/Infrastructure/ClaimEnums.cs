using System;

namespace FAS.WebUI.Infrastructure
{
    [Flags]
    public enum Permission
    {
        Create = 1,
        Read = 2,
        Update = 4,
        Delete = 8,
    }

    public enum Target
    {
        Account,
        Home
    }
}