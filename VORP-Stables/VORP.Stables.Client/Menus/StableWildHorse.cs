using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace VORP.Stables.Client.Menus
{
    internal class StableWildHorse : BaseScript
    {
        public static StableWildHorse CreateInstance()
        {
            return new StableWildHorse();
        }

        public static async void SaveWildHorseInStable(string title)
        {
            var playerId = API.GetPlayerIndex();
            var playerPed = API.GetPlayerPed(playerId);
            var horseEntity = API.GetMount(playerPed);
            var horsePed = API.GetEntityModel(horseEntity);
            var horseModelString = "nothing";

            var pedList = new List<string>
            {
                "A_C_Horse_AmericanPaint_Greyovero",
                "A_C_Horse_AmericanPaint_Overo",
                "A_C_Horse_AmericanPaint_SplashedWhite",
                "A_C_Horse_AmericanPaint_Tobiano",
                "A_C_Horse_AmericanStandardbred_Black",
                "A_C_Horse_AmericanStandardbred_Buckskin",
                "A_C_Horse_AmericanStandardbred_PalominoDapple",
                "A_C_Horse_AmericanStandardbred_SilverTailBuckskin",
                "A_C_Horse_Andalusian_DarkBay",
                "A_C_Horse_Andalusian_Perlino",
                "A_C_Horse_Andalusian_RoseGray",
                "A_C_Horse_Appaloosa_BlackSnowflake",
                "A_C_Horse_Appaloosa_Blanket",
                "A_C_Horse_Appaloosa_BrownLeopard",
                "A_C_Horse_Appaloosa_FewSpotted_PC",
                "A_C_Horse_Appaloosa_Leopard",
                "A_C_Horse_Appaloosa_LeopardBlanket",
                "A_C_Horse_Arabian_Black",
                "A_C_Horse_Arabian_Grey",
                "A_C_Horse_Arabian_RedChestnut",
                "A_C_Horse_Arabian_RoseGreyBay",
                "A_C_Horse_Arabian_WarpedBrindle_PC",
                "A_C_Horse_Arabian_White",
                "A_C_Horse_Ardennes_BayRoan",
                "A_C_Horse_Ardennes_IronGreyRoan",
                "A_C_Horse_Ardennes_StrawberryRoan",
                "A_C_Horse_Belgian_BlondChestnut",
                "A_C_Horse_Belgian_MealyChestnut",
                "A_C_Horse_DutchWarmblood_ChocolateRoan",
                "A_C_Horse_DutchWarmblood_SealBrown",
                "A_C_Horse_DutchWarmblood_SootyBuckskin",
                "A_C_Horse_HungarianHalfbred_DarkDappleGrey",
                "A_C_Horse_HungarianHalfbred_FlaxenChestnut",
                "A_C_Horse_HungarianHalfbred_LiverChestnut",
                "A_C_Horse_HungarianHalfbred_PiebaldTobiano",
                "A_C_Horse_KentuckySaddle_Black",
                "A_C_Horse_KentuckySaddle_ButterMilkBuckskin_PC",
                "A_C_Horse_KentuckySaddle_ChestnutPinto",
                "A_C_Horse_KentuckySaddle_Grey",
                "A_C_Horse_KentuckySaddle_SilverBay",
                "A_C_Horse_Kladruber_Black",
                "A_C_Horse_Kladruber_Silver",
                "A_C_Horse_Kladruber_Cremello",
                "A_C_Horse_Kladruber_Grey",
                "A_C_Horse_Kladruber_DappleRoseGrey",
                "A_C_Horse_Kladruber_White",
                "A_C_Horse_MissouriFoxTrotter_AmberChampagne",
                "A_C_Horse_MissouriFoxTrotter_SableChampagne",
                "A_C_Horse_MissouriFoxTrotter_SilverDapplePinto",
                "A_C_Horse_Morgan_Bay",
                "A_C_Horse_Morgan_BayRoan",
                "A_C_Horse_Morgan_FlaxenChestnut",
                "A_C_Horse_Morgan_LiverChestnut_PC",
                "A_C_Horse_Morgan_Palomino",
                "A_C_Horse_MP_Mangy_Backup",
                "A_C_Horse_Mustang_GoldenDun",
                "A_C_Horse_Mustang_GrulloDun",
                "A_C_Horse_Mustang_TigerStripedBay",
                "A_C_Horse_Mustang_WildBay",
                "A_C_Horse_Nokota_BlueRoan",
                "A_C_Horse_Nokota_ReverseDappleRoan",
                "A_C_Horse_Nokota_WhiteRoan",
                "A_C_Horse_Shire_DarkBay",
                "A_C_Horse_Shire_LightGrey",
                "A_C_Horse_Shire_RavenBlack",
                "A_C_Horse_SuffolkPunch_RedChestnut",
                "A_C_Horse_SuffolkPunch_Sorrel",
                "A_C_Horse_TennesseeWalker_BlackRabicano",
                "A_C_Horse_TennesseeWalker_Chestnut",
                "A_C_Horse_TennesseeWalker_DappleBay",
                "A_C_Horse_TennesseeWalker_FlaxenRoan",
                "A_C_Horse_TennesseeWalker_MahoganyBay",
                "A_C_Horse_TennesseeWalker_RedRoan",
                "A_C_Horse_TennesseeWalker_GoldPalomino_PC",
                "A_C_Horse_Thoroughbred_BlackChestnut",
                "A_C_Horse_Thoroughbred_BloodBay",
                "A_C_Horse_Thoroughbred_Brindle",
                "A_C_Horse_Thoroughbred_DappleGrey",
                "A_C_Horse_Thoroughbred_ReverseDappleBlack",
                "A_C_Horse_Turkoman_DarkBay",
                "A_C_Horse_Turkoman_Gold",
                "A_C_Horse_Turkoman_Silver",
                "A_C_Horse_Criollo_Dun",
                "A_C_Horse_Criollo_MarbleSabino",
                "A_C_Horse_Criollo_BayFrameOvero",
                "A_C_Horse_Criollo_BayBrindle",
                "A_C_Horse_Criollo_SorrelOvero",
                "A_C_Horse_Criollo_BlueRoanOvero",
                "A_C_Horse_Breton_SteelGrey",
                "A_C_Horse_Breton_MealyDapple",
                "A_C_Horse_Breton_SealBrown",
                "A_C_Horse_Breton_GrulloDun",
                "A_C_Horse_Breton_Sorrel",
                "A_C_Horse_Breton_RedRoan",
                "A_C_HORSE_NORFOLKROADSTER_BLACK",
                "A_C_HORSE_NORFOLKROADSTER_SPECKLEDGREY",
                "A_C_HORSE_NORFOLKROADSTER_PIEBALDROAN",
                "A_C_HORSE_NORFOLKROADSTER_ROSEGREY",
                "A_C_HORSE_NORFOLKROADSTER_DAPPLEDBUCKSKIN",
                "A_C_HORSE_NORFOLKROADSTER_SPOTTEDTRICOLOR",
                "A_C_HorseMulePainted_01",
                "A_C_HorseMule_01",
                "A_C_Donkey_01"
            };

            foreach (var pedString in pedList.Where(pedString =>
                API.GetHashKey(pedString.ToUpper()).Equals(horsePed)))
            {
                horseModelString = pedString;
            }

            await DeleteHorse(horseEntity);
            
            StablesShop.horsecost = 0.00;
            StablesShop.horsemodel = horseModelString;
            await StablesShop.ConfirmBuyHorse(title);
        }
        
        private static async Task DeleteHorse(int horsePed)
        {
            var timeout = 0;
            API.NetworkRequestControlOfEntity(horsePed);
            API.SetEntityAsMissionEntity(horsePed, true, true);

            while (API.DoesEntityExist(horsePed) && timeout < 10)
            {
                API.DeletePed(ref horsePed);
                API.DeleteEntity(ref horsePed);

                if (!API.DoesEntityExist(horsePed))
                {
                    Debug.WriteLine("Horse despawn");
                }

                await Delay(500);

                timeout += 1;

                if (API.DoesEntityExist(horsePed) && timeout == 9)
                {
                    Debug.WriteLine("Horse can't despawn");
                }
            }
        }
    }
}