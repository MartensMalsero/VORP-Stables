using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
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

        public static async void SaveWildHorseInStable(string title)
        {
            var playerId = API.GetPlayerIndex();
            var playerPed = API.GetPlayerPed(playerId);
            var mounted = API.GetMount(playerPed);
            var horsePed = API.GetEntityModel(mounted);
            var horseModelString = "nothing";

            var pedList = new List<string>
            {
                "A_C_HORSE_AMERICANPAINT_GREYOVERO",
                "A_C_HORSE_AMERICANPAINT_OVERO",
                "A_C_HORSE_AMERICANPAINT_SPLASHEDWHITE",
                "A_C_HORSE_AMERICANPAINT_TOBIANO",
                "A_C_HORSE_AMERICANSTANDARDBRED_BLACK",
                "A_C_HORSE_AMERICANSTANDARDBRED_BUCKSKIN",
                "A_C_HORSE_AMERICANSTANDARDBRED_LIGHTBUCKSKIN",
                "A_C_HORSE_AMERICANSTANDARDBRED_PALOMINODAPPLE",
                "A_C_HORSE_AMERICANSTANDARDBRED_SILVERTAILBUCKSKIN",
                "A_C_HORSE_ANDALUSIAN_DARKBAY",
                "A_C_HORSE_ANDALUSIAN_PERLINO",
                "A_C_HORSE_ANDALUSIAN_ROSEGRAY",
                "A_C_HORSE_APPALOOSA_BLACKSNOWFLAKE",
                "A_C_HORSE_APPALOOSA_BLANKET",
                "A_C_HORSE_APPALOOSA_BROWNLEOPARD",
                "A_C_HORSE_APPALOOSA_FEWSPOTTED_PC",
                "A_C_HORSE_APPALOOSA_LEOPARD",
                "A_C_HORSE_APPALOOSA_LEOPARDBLANKET",
                "A_C_HORSE_ARABIAN_BLACK",
                "A_C_HORSE_ARABIAN_GREY",
                "A_C_HORSE_ARABIAN_REDCHESTNUT_PC",
                "A_C_HORSE_ARABIAN_REDCHESTNUT",
                "A_C_HORSE_ARABIAN_ROSEGREYBAY",
                "A_C_HORSE_ARABIAN_WARPEDBRINDLE_PC",
                "A_C_HORSE_ARABIAN_WHITE",
                "A_C_HORSE_ARDENNES_BAYROAN",
                "A_C_HORSE_ARDENNES_IRONGREYROAN",
                "A_C_HORSE_ARDENNES_STRAWBERRYROAN",
                "A_C_HORSE_BELGIAN_BLONDCHESTNUT",
                "A_C_HORSE_BELGIAN_MEALYCHESTNUT",
                "A_C_HORSE_BRETON_GRULLODUN",
                "A_C_HORSE_BRETON_MEALYDAPPLEBAY",
                "A_C_HORSE_BRETON_REDROAN",
                "A_C_HORSE_BRETON_SEALBROWN",
                "A_C_HORSE_BRETON_SORREL",
                "A_C_HORSE_BRETON_STEELGREY",
                "A_C_HORSE_BUELL_WARVETS",
                "A_C_HORSE_CRIOLLO_BAYBRINDLE",
                "A_C_HORSE_CRIOLLO_BAYFRAMEOVERO",
                "A_C_HORSE_CRIOLLO_BLUEROANOVERO",
                "A_C_HORSE_CRIOLLO_DUN",
                "A_C_HORSE_CRIOLLO_MARBLESABINO",
                "A_C_HORSE_CRIOLLO_SORRELOVERO",
                "A_C_HORSE_DUTCHWARMBLOOD_CHOCOLATEROAN",
                "A_C_HORSE_DUTCHWARMBLOOD_SEALBROWN",
                "A_C_HORSE_DUTCHWARMBLOOD_SOOTYBUCKSKIN",
                "A_C_HORSE_EAGLEFLIES",
                "A_C_HORSE_GANG_BILL",
                "A_C_HORSE_GANG_CHARLES_ENDLESSSUMMER",
                "A_C_HORSE_GANG_CHARLES",
                "A_C_HORSE_GANG_DUTCH",
                "A_C_HORSE_GANG_HOSEA",
                "A_C_HORSE_GANG_JAVIER",
                "A_C_HORSE_GANG_JOHN",
                "A_C_HORSE_GANG_KAREN",
                "A_C_HORSE_GANG_KIERAN",
                "A_C_HORSE_GANG_LENNY",
                "A_C_HORSE_GANG_MICAH",
                "A_C_HORSE_GANG_SADIE_ENDLESSSUMMER",
                "A_C_HORSE_GANG_SADIE",
                "A_C_HORSE_GANG_SEAN",
                "A_C_HORSE_GANG_TRELAWNEY",
                "A_C_HORSE_GANG_UNCLE_ENDLESSSUMMER",
                "A_C_HORSE_GANG_UNCLE",
                "A_C_HORSE_HUNGARIANHALFBRED_DARKDAPPLEGREY",
                "A_C_HORSE_HUNGARIANHALFBRED_FLAXENCHESTNUT",
                "A_C_HORSE_HUNGARIANHALFBRED_LIVERCHESTNUT",
                "A_C_HORSE_HUNGARIANHALFBRED_PIEBALDTOBIANO",
                "A_C_HORSE_JOHN_ENDLESSSUMMER",
                "A_C_HORSE_KENTUCKYSADDLE_BLACK",
                "A_C_HORSE_KENTUCKYSADDLE_BUTTERMILKBUCKSKIN_PC",
                "A_C_HORSE_KENTUCKYSADDLE_CHESTNUTPINTO",
                "A_C_HORSE_KENTUCKYSADDLE_GREY",
                "A_C_HORSE_KENTUCKYSADDLE_SILVERBAY",
                "A_C_HORSE_KLADRUBER_BLACK",
                "A_C_HORSE_KLADRUBER_CREMELLO",
                "A_C_HORSE_KLADRUBER_DAPPLEROSEGREY",
                "A_C_HORSE_KLADRUBER_GREY",
                "A_C_HORSE_KLADRUBER_SILVER",
                "A_C_HORSE_KLADRUBER_WHITE",
                "A_C_HORSE_MISSOURIFOXTROTTER_AMBERCHAMPAGNE",
                "A_C_HORSE_MISSOURIFOXTROTTER_SABLECHAMPAGNE",
                "A_C_HORSE_MISSOURIFOXTROTTER_SILVERDAPPLEPINTO",
                "A_C_HORSE_MORGAN_BAY",
                "A_C_HORSE_MORGAN_BAYROAN",
                "A_C_HORSE_MORGAN_FLAXENCHESTNUT",
                "A_C_HORSE_MORGAN_LIVERCHESTNUT_PC",
                "A_C_HORSE_MORGAN_PALOMINO",
                "A_C_HORSE_MP_MANGY_BACKUP",
                "A_C_HORSE_MURFREEBROOD_MANGE_01",
                "A_C_HORSE_MURFREEBROOD_MANGE_02",
                "A_C_HORSE_MURFREEBROOD_MANGE_03",
                "A_C_HORSE_MUSTANG_GOLDENDUN",
                "A_C_HORSE_MUSTANG_GRULLODUN",
                "A_C_HORSE_MUSTANG_TIGERSTRIPEDBAY",
                "A_C_HORSE_MUSTANG_WILDBAY",
                "A_C_HORSE_NOKOTA_BLUEROAN",
                "A_C_HORSE_NOKOTA_REVERSEDAPPLEROAN",
                "A_C_HORSE_NOKOTA_WHITEROAN",
                "A_C_HORSE_SHIRE_DARKBAY",
                "A_C_HORSE_SHIRE_LIGHTGREY",
                "A_C_HORSE_SHIRE_RAVENBLACK",
                "A_C_HORSE_SUFFOLKPUNCH_REDCHESTNUT",
                "A_C_HORSE_SUFFOLKPUNCH_SORREL",
                "A_C_HORSE_TENNESSEEWALKER_BLACKRABICANO",
                "A_C_HORSE_TENNESSEEWALKER_CHESTNUT",
                "A_C_HORSE_TENNESSEEWALKER_DAPPLEBAY",
                "A_C_HORSE_TENNESSEEWALKER_FLAXENROAN",
                "A_C_HORSE_TENNESSEEWALKER_GOLDPALOMINO_PC",
                "A_C_HORSE_TENNESSEEWALKER_MAHOGANYBAY",
                "A_C_HORSE_TENNESSEEWALKER_REDROAN",
                "A_C_HORSE_THOROUGHBRED_BLACKCHESTNUT",
                "A_C_HORSE_THOROUGHBRED_BLOODBAY",
                "A_C_HORSE_THOROUGHBRED_BRINDLE",
                "A_C_HORSE_THOROUGHBRED_DAPPLEGREY",
                "A_C_HORSE_THOROUGHBRED_REVERSEDAPPLEBLACK",
                "A_C_HORSE_TURKOMAN_DARKBAY",
                "A_C_HORSE_TURKOMAN_GOLD",
                "A_C_HORSE_TURKOMAN_SILVER",
                "A_C_HORSE_WINTER02_01",
                "A_C_HORSEMULE_01",
                "A_C_HORSEMULEPAINTED_01",
            };

            foreach (var pedString in pedList.Where(pedString =>
                API.GetHashKey(pedString).Equals(horsePed)))
            {
                horseModelString = pedString;
            }

            //ToDo delete mounted horse
            await HorseManagment.DeleteHorseTest(horsePed);
            
            //ToDo uncomment to put into DB
            //ToDo reload stable entries not working at this moment
            /*StablesShop.horsecost = 0.00;
            StablesShop.horsemodel = horseModelString;
            await StablesShop.ConfirmBuyHorse(title);*/
        }

        private static async Task DeleteHorse(int horsePed)
        {
            var timeout = 0;
            
            while (API.DoesEntityExist(horsePed) && timeout < 10)
            {
                API.DeletePed(ref horsePed);
                API.DeleteEntity(ref horsePed);

                if (!API.DoesEntityExist(horsePed))
                {
                    Debug.WriteLine("Horse despawn");
                }

                await Task.Delay(500);

                timeout += 1;

                if (API.DoesEntityExist(horsePed) && timeout == 9)
                {
                    Debug.WriteLine("Horse can't despawn");
                }
            }
        }
    }
}