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
        private uint _difficulty;
        public uint Difficulty
        {
            get { return _difficulty; }
            set { _difficulty = value; if (_difficulty >= 16) _difficulty = 15; }
        }
        public override void StartTurn()
        {

            CommandSystem.Instance.Listen(() => GetStockFishResponce());

        }

        private string GetStockFishResponce()
        {
            string fen = ChessExtentionMethods.ToFENFromat(Engine.CurrentScene);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://stockfish.online/api/s/v2.php?fen={fen}&depth={_difficulty}");
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
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
        public string evaluation;
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
