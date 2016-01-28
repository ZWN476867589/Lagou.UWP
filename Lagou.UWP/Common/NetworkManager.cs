using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

/*
http://stackoverflow.com/questions/24003786/how-to-get-connection-and-carrier-information-in-windows-phone-8-1-runtime
*/

namespace Lagou.UWP.Common {
    public class NetworkManager : PropertyChangedBase {


        private NetworkTypes _networkType = NetworkTypes.None;
        public NetworkTypes NetworkType {
            get {
                return this._networkType;
            }
            private set {
                this._networkType = value;
                this.NotifyOfPropertyChange(() => this.NetworkType);
            }
        }

        public static Lazy<NetworkManager> Instance = new Lazy<NetworkManager>(() => new NetworkManager());

        static NetworkManager() {
            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;
            GetNetworkType();
        }

        private NetworkManager() {

        }

        private static void NetworkInformation_NetworkStatusChanged(object sender) {
            Instance.Value.NetworkType = GetNetworkType();
        }

        private static NetworkTypes GetNetworkType() {
            var profile = NetworkInformation.GetInternetConnectionProfile();

            if (profile.IsWwanConnectionProfile) {
                var c = profile.WwanConnectionProfileDetails.GetCurrentDataClass();
                // 0:2G 1:3G 2:4G  3:wifi  4:无连接
                switch (c) {
                    //2G-equivalent
                    case WwanDataClass.Edge:
                    case WwanDataClass.Gprs:
                        return NetworkTypes.G2;
                    //3G-equivalent
                    case WwanDataClass.Cdma1xEvdo:
                    case WwanDataClass.Cdma1xEvdoRevA:
                    case WwanDataClass.Cdma1xEvdoRevB:
                    case WwanDataClass.Cdma1xEvdv:
                    case WwanDataClass.Cdma1xRtt:
                    case WwanDataClass.Cdma3xRtt:
                    case WwanDataClass.CdmaUmb:
                    case WwanDataClass.Umts:
                    case WwanDataClass.Hsdpa:
                    case WwanDataClass.Hsupa:
                        return NetworkTypes.G2;
                    //4G-equivalent
                    case WwanDataClass.LteAdvanced:
                        return NetworkTypes.G4;
                    //not connected
                    case WwanDataClass.None:
                    case WwanDataClass.Custom:
                    default:
                        return NetworkTypes.None;
                }
            } else
                return NetworkTypes.Wifi;
        }

        public enum NetworkTypes {
            None,
            G2,
            G3,
            G4,
            Wifi
        }
    }
}
