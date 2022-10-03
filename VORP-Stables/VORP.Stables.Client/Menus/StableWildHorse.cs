using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace VORP.Stables.Client.Menus
{
    internal class StableWildHorse
    {
        public static StableWildHorse CreateInstance()
        {
            return new StableWildHorse();
        }

        public static async void SaveWildHorseInStable(string title, dynamic user)
        {
            if (API.IsPedOnMount(API.GetPlayerPed(API.GetPlayerIndex()))) Debug.WriteLine("mounted!");
            Debug.WriteLine($"TEST: {API.GetMount(API.GetPlayerPed(API.GetPlayerIndex()))}");

            var playerId = API.GetPlayerIndex();
            var playerPed = API.GetPlayerPed(playerId);
            var mounted = API.GetMount(playerPed);

            //TESTEN!s
            var model = API.GetEntityModel(mounted);
            Debug.WriteLine($"TEST ENTITY MODEL: {model}");

            //ToDo ConfirmButtonText ändern
            StablesShop.horsecost = 0.00;
            StablesShop.horsemodel = "";
            await StablesShop.ConfirmBuyHorse(title);
        }
    }
}