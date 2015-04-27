using System;
using System.Linq;
using System.Net;
using DotRas;
using Model;

namespace Core
{
    public class VpnConnectionProvider : IVpnConnectionProvider
    {
        private readonly ILoginMonitor _loginMonitor;
        private readonly RasDialer _dialer;
        private readonly RasPhoneBook _phoneBook;
        private RasEntry _entry;

        public VpnConnectionProvider(ILoginMonitor loginMonitor)
        {
            _loginMonitor = loginMonitor;
            _phoneBook = new RasPhoneBook();
            _dialer = new RasDialer
            {
                PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.User),
                Credentials = new NetworkCredential(_loginMonitor.Username, _loginMonitor.Password)
            };
        }

        public bool Connect(VpnConnection selectedConnection)
        {
            _dialer.Credentials = new NetworkCredential(_loginMonitor.Username, _loginMonitor.Password);
            try
            {
                string path = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.User);
                _phoneBook.Open(path);

                _entry = _phoneBook.Entries
                    .FirstOrDefault(x => x.PhoneNumber == selectedConnection.Hostname);

                if (_entry != null)
                {
                    _dialer.EntryName = _entry.Name;
                    _dialer.Dial();
                    return true;
                }
            }
            catch (InvalidOperationException e)
            {
                Disconnect();
            }
            catch (RasException e)
            {
                Disconnect();
            }

            return false;
        }

        public void Disconnect()
        {
            RasConnection connection = RasConnection.GetActiveConnections()
                .FirstOrDefault(x => x.EntryName == _entry.Name);
            if (connection != null) connection.HangUp();
        }
    }
}