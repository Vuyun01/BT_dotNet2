namespace DemoMVC.Models.GiaiPT{
    public class GiaiPT{
        public static string GiaiPTbac1(string a, string b){
            double hsa = Convert.ToDouble(a);
                double hsb = Convert.ToDouble(b);
                string thongbao = "";

                if (hsa == 0)
                {
                    if (hsb == 0) thongbao = "PT vo so nghiem";
                    else thongbao = "PT vo nghiem";
                }
                else
                {

                    double result = -hsb / hsa;
                    thongbao = $"PT co nghiem X = {result}";
                }
                return thongbao;
        }
        public static string GiaiPTbac2(string a, string b, string c)
            {
                double hsa = Convert.ToDouble(a);
                double hsb = Convert.ToDouble(b);
                double hsc = Convert.ToDouble(c);
                double result;
                string thongbao = "";
                double x1, x2;

                double delta = hsb * hsb - 4 * hsa * hsc;
                if (hsa == 0)
                {
                    if (hsb == 0)
                    {
                        if (hsc == 0) thongbao = "PT vo so nghiem";
                        else thongbao = "PT vo nghiem";
                    }
                    else
                    {
                        result = (-hsc) / hsb;
                        thongbao = $"Phuong trinh co nghiem {result}";
                    }
                }
                else
                {
                    if (delta > 0)
                    {
                        x1 = (-(hsb * hsb) + Math.Sqrt(delta)) / 2 * hsa;
                        x2 = (-(hsb * hsb) - Math.Sqrt(delta)) / 2 * hsa;
                        thongbao = $"Phuong trinh co 2 nghiem X1 = {x1} , X2 = {x2}";
                    }
                    else if (delta < 0)
                    {
                        thongbao = "Phuong trinh vo nghiem";
                    }
                    else
                    {
                        x1 = (-hsc) / 2 * hsa;
                        thongbao = $"Phuong trinh co nghiem X = {x1}";
                    }

                }
                return thongbao;
            }
    }

}
    
