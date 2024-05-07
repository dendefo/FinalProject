using Core;
using Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChessDemo
{
    public class StockFish : ChessActor
    {
        public override void StartTurn()
        {
            
            CommandSystem.Instance.Listen(() => GetStockFishResponce());

        }

        private string GetStockFishResponce()
        {
            string fen = ChessExtentionMethods.ToFENFromat(Engine.CurrentScene);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://stockfish.online/api/s/v2.php?fen={fen}&depth=10");
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                using (var reader = new System.IO.StreamReader(stream))
                {
                    string responseString = reader.ReadToEnd();
                    StockFishResponce FishResponce = Newtonsoft.Json.JsonConvert.DeserializeObject<StockFishResponce>(responseString);

                    return "StockFish " + FishResponce.GetMove();
                }
            }
        }
    }
    public struct StockFishResponce
    {
        public bool success;
        public float evaluation;
        public string mate;
        public string bestmove;
        public string continuation;
        public string GetMove()
        {
            var strings = bestmove.Split(' ');
            return strings[1];
        }
    }
}
