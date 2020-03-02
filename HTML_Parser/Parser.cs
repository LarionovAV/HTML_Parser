using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace HTML_Parser
{
    public class Parser
    {
        private Dictionary<int, string> romeNums = new Dictionary<int, string>();
        private static Parser instance = null;
        private Parser() {
            romeNums.Add(1, "I");
            romeNums.Add(2, "II");
            romeNums.Add(3, "III");
            romeNums.Add(4, "IV");
            romeNums.Add(5, "V");
            romeNums.Add(6, "VI");
            romeNums.Add(7, "VII");
            romeNums.Add(8, "VIII");
            romeNums.Add(9, "IX");
        }

        public static Parser GetInstance() {
            if (instance == null)
                instance = new Parser();
            return instance;
        }

        /*
         * возвращает словарь словарей 
         * -KEY: уровень образования
         * -VALUE: словарь профессий в медицине, соответствующих уровню образования
         *      -VALUE.KEY: код профессии
         *      -VALUE.VALUE: наименование провессии
         */
        public Dictionary<string, Dictionary<string, string>> ParseProffessions() {

            string[] profLevels = new string[9];
            //Инициализируем озвращаемый объект
            Dictionary<string, Dictionary<string, string>> returnItem = new Dictionary<string, Dictionary<string, string>>();

            //Создаем объект класса HtmlWeb для получения HTML-страниц
            HtmlWeb web = new HtmlWeb();
            //получаем html страницу (в данном случае список уровней образования)
            HtmlDocument document = web.Load("https://classifikators.ru/okso");

            //Получаем HTML узлы, соответствующие XPATH запросу
            //Синтаксис XPATH см. в Интернете (например, здесь https://habr.com/ru/post/114772/)
            //Путь проложили до строк
            var tableNodes = document.DocumentNode.SelectNodes(".//*[@class = 'container']/div[@class = 'row']/div/div[@class='tab-content']/div/table[@class='table table-hover table-bordered']/tbody/tr");
            if (tableNodes == null)
                return returnItem;
            foreach (var tItem in tableNodes) {
                int i = romeNums.Where(x => x.Value == tItem.ChildNodes[0].InnerText).FirstOrDefault().Key;
                profLevels[i - 1] = tItem.ChildNodes[1].ChildNodes[0].InnerText;
            }

            for (int i = 1; i <= 9; i++) {
                //Инициализируем словарь профессий
                Dictionary<string, string> parsingResult = new Dictionary<string, string>();
                string profLevel;
                if (!romeNums.TryGetValue(i, out profLevel))
                    continue;
                //загружаем страницу с профессиями нужного уровня образования
                document = web.Load("https://classifikators.ru/okso/" + profLevel + ".3");
                //парсим ее 
                tableNodes = document.DocumentNode.SelectNodes(".//*[@class = 'container']/div[@class = 'row']/div/table[@class='table table-hover table-bordered']/tbody/tr");
                if (tableNodes == null)
                    continue;
                foreach (var tItem in tableNodes) {
                    //Переходим на уровень ниже
                    HtmlDocument lowLevelDocument = web.Load("https://classifikators.ru" + tItem.ChildNodes[1].ChildNodes[0].Attributes["href"].Value);
                    var lowTableNodes = lowLevelDocument.DocumentNode.SelectNodes(".//*[@class = 'container']/div[@class = 'row']/div/table[@class='table table-hover table-bordered']/tbody/tr");
                    foreach (var lowTabItem in lowTableNodes){
                        parsingResult.Add(lowTabItem.ChildNodes[0].InnerText, lowTabItem.ChildNodes[1].ChildNodes[0].InnerText);
                    }
                }
                returnItem.Add(profLevels[i - 1], parsingResult);
            }
            

            return returnItem;
        }

        public Dictionary<string, string> ParseCitizenships() {
            Dictionary<string, string> citizenships = new Dictionary<string, string>();


            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load("https://classifikators.ru/oksm");

            var tableNodes = document.DocumentNode.SelectNodes(".//*[@class = 'table table-condensed table-hover table-ok table-numbered']/tbody/tr");
            if (tableNodes == null)
                return citizenships;
            foreach (var tItem in tableNodes)
            {
                citizenships.Add(tItem.ChildNodes[3].ChildNodes[0].InnerText, tItem.ChildNodes[5].InnerText);
            }
            return citizenships;
        }
    }
}
