using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using QECReg.DB;

namespace QECReg
{
    public partial class gift : System.Web.UI.Page
    {

        QECDataContext DB = new QECDataContext();
        QEC1DataContext DB1 = new QEC1DataContext();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btngift_Click(object sender, EventArgs e)
        {
            try
            {
                int[] gem = new int[5] { 10000, 5000, 1000, 250, 0 };  //各等级的玩家每日发放的钻石数
                string[] group = new string[5]{"豪R","大R","中R","小R","非R"};  //各等级玩家称谓
                int sum = 0;   //各等级发放礼包个数
                for (int g = 1; g <= 5; g++)   //从1-5等级循环发放每日钻石礼包
                {
                    var test = from t in DB.t_testuser where t.groupid == g && t.gifttimes > 0 select t;//从封测玩家表中选出相应等级并且剩余礼包发放次数大于0的玩家列表
                    foreach (var i in test)//遍历该列表
                    {
                        
                        var inf = from u in DB1.t_user_info where u.user_id == i.user_id select u;//选取玩家信息表中对应ID的玩家信息
                        if (inf.Count() > 0)//如果该玩家信息存在
                        {
                            inf.First().user_gem += gem[g - 1];//玩家钻石增加相应数量
                            i.gifttimes--;//玩家礼包剩余发放次数减1
                            sum++;//各等级发放礼包个数加1
                        }

                    }
                    DB.SubmitChanges();//提交数据库更改
                    DB1.SubmitChanges();
                    Response.Write("<h2>成功发放"+sum+"个"+group[g-1]+"礼包!</h2>");//前端页面输出该等级礼包发放个数
                    sum = 0;//礼包数量清零
                }
                ScriptManager.RegisterStartupScript(this, typeof(Page), "tip", "alert( '礼包发放成功！');", true);//所有等级玩家礼包发放完成后，弹出提示信息
                
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "tip", "alert( '礼包发放失败！');", true);//如果程序出错，则弹出错误信息
            }
        }
    }
}