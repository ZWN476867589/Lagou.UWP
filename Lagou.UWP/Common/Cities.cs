using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.UWP.Common {
    public static class Cities {

        //拼音用翻译软件得出，这样就不会出现多音字错音的问题了。
        private static readonly string Datas = "保定,北京,包头,长春,成都,重庆,长沙,常熟,朝阳,常州,东莞,大连,德州,佛山,福州,桂林,贵阳,广州,淮安,哈尔滨,合肥,黄冈,呼和浩特,海口,淮南,杭州,惠州,湖州,金华,吉林,济南,济宁,嘉兴,江阴,昆明,昆山,廊坊,丽江,拉萨,临沂,洛阳,兰州,柳州,马鞍山,茂名,绵阳,宁波,南昌,南京,南宁,南通,莆田,青岛,秦皇岛,泉州,上海,石家庄,汕头,绍兴,沈阳,三亚,深圳,苏州,泰安,天津,唐山,太原,台州,潍坊,武汉,芜湖,威海,乌鲁木齐,无锡,温州,西安,香港,厦门,西宁,邢台,湘潭,徐州,银川,盐城,宜昌,运城,烟台,玉溪,扬州,淄博,珠海,镇江,张家港,肇庆,中山,郑州,漳州";
        private static readonly string Datas2 = "bao ding ,bei jing ,bao tou ,chang chun ,cheng du ,chong qing ,chang sha ,chang shu ,chao yang ,chang zhou ,dong guan ,da lian ,de zhou ,fo shan ,fu zhou ,gui lin ,gui yang ,guang zhou ,huai an ,ha er bin ,he fei ,huang gang ,hu he hao te ,hai kou ,huai nan ,hang zhou ,hui zhou ,hu zhou ,jin hua ,ji lin ,ji nan ,ji ning ,jia xing ,jiang yin ,kun ming ,kun shan ,lang fang ,li jiang ,la sa ,lin yi ,luo yang ,lan zhou ,liu zhou ,ma an shan ,mao ming ,mian yang ,ning bo ,nan chang ,nan jing ,nan ning ,nan tong ,pu tian ,qing dao ,qin huang dao ,quan zhou ,shang hai ,shi jia zhuang ,shan tou ,shao xing ,shen yang ,san ya ,shen zhen ,su zhou ,tai an ,tian jin ,tang shan ,tai yuan ,tai zhou ,wei fang ,wu han ,wu hu ,wei hai ,wu lu mu qi ,wu xi ,wen zhou ,xi an ,xiang gang ,xia men ,xi ning ,xing tai ,xiang tan ,xu zhou ,yin chuan ,yan cheng ,yi chang ,yun cheng ,yan tai ,yu xi ,yang zhou ,zi bo ,zhu hai ,zhen jiang ,zhang jia gang ,zhao qing ,zhong shan ,zheng zhou ,zhang zhou";

        public static readonly List<City> Items;

        static Cities() {
            Items = new List<City>();
            var cities = Datas.Split(',');
            var pys = Datas2.Split(',');
            for (var i = 0; i < cities.Length; i++) {
                var c = cities[i];
                var p = pys[i];
                Items.Add(new City() {
                    Name = c,
                    PY = p
                });
            }
        }
    }

    public class City {
        public string Name { get; set; }
        public string PY { get; set; }
    }
}
