using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace ngschaiderBot
{

    class UrbanDictionary
    {

        public class List
        {
            public string definition { get; set; }
            public string permalink { get; set; }
            public int thumbs_up { get; set; }
            public string author { get; set; }
            public string word { get; set; }
            public int defid { get; set; }
            public string current_vote { get; set; }
            public string example { get; set; }
            public int thumbs_down { get; set; }
        }

        public class SearchResult
        {
            public List<string> tags { get; set; }
            public string result_type { get; set; }
            public List<List> list { get; set; }
            public List<string> sounds { get; set; }
        }

        public static SearchResult Search(string word)
        {
            string json = Utils.GetRequest($"http://api.urbandictionary.com/v0/define?term={word}");
            SearchResult root = JsonConvert.DeserializeObject<SearchResult>(json);

            return root;
        }

    }

    
}
