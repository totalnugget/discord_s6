using System;
using System.Collections.Generic;
using System.Text;

namespace GuildService.Domain.Enums
{

    [Flags]
    public enum Permissions : int
    {
        none = 0,
        ViewChannels = 1 << 0,
        ManageChannels = 1 << 1,
        ManageServer = 1 << 2,
        SendMessages = 1 << 3,
        ManageMessages = 1 << 4,
        ChangeNickname = 1 << 5,
        ManageNicknames = 1 << 6,
        all = int.MaxValue

    }
}
