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
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        QECDataContext DB = new QECDataContext();
        protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {

            string uName = txtName.Text.Trim();
            string password = txtPassword.Text.Trim();
            string password2 = txtPassword2.Text.Trim();
            string mail = txtmail.Text.Trim();
            string qq = txtqq.Text.Trim();
            string code = txtjihuoma.Text.Trim().ToLower();

            if (verifyUser(uName))
            {
                string password_MD5 = userMd5(password);

                if (CreateUser(uName, password_MD5))
                {


                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "tip", "alert( '注册成功！');", true);
                    Response.Write("<script>window.location.href='succeed.aspx'</script>");

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "tip", "alert( '抱歉！注册失败，请重新尝试！');", true);
                }

            }
        }


        private bool verifyUser(string uName)   //对输入的数据进行注册前验证
        {

            string password = txtPassword.Text.Trim();
            string password2 = txtPassword2.Text.Trim();
            string mail = txtmail.Text.Trim();
            string qq = txtqq.Text.Trim();
            string code = txtjihuoma.Text.Trim().ToLower();

            int count = (from u in DB.t_user where u.user_name == uName select u).Count();
            int count1 = (from c in DB.t_actcode where c.actcode == code select c).Count();
            int count2 = (from t in DB.t_user where t.email == mail select t).Count();

            if (Regex.IsMatch(code, @"^[0-9]{2}[0-9a-z]{4}[1-5]$") == false || count1 <= 0)  //验证激活码是否有效
            {
                lblCodeError.Text = "× 激活码无效";
                lblCodeError.Style["color"] = "red";
                return false;
            }
            else
            {
                lblCodeError.Text = "√";
                lblCodeError.Style["color"] = "white";
            }

            if (Regex.IsMatch(uName, @"^\w{3,13}$") == false || Regex.IsMatch(uName, @"[\u4E00-\u9FA5]+") == true)
            {
                lblNameError.Text = "× 请输入3-13位用户名，可包含数字、字母、下划线，请勿使用中文字符以及特殊符号";
                lblNameError.Style["color"] = "red";
                return false;
            }
            else if (count > 0)
            {
                lblNameError.Text = "用户名已被使用";
                lblNameError.Style["color"] = "red";
                return false;
            }
            else
            {
                lblNameError.Text = "√";
                lblNameError.Style["color"] = "white";
            }

            if (txtPassword.Text.Trim().Length < 6 || txtPassword.Text.Trim().Length > 20)
            {
                lblPassError.Text = "× 请输入6-20位用户密码！";
                lblPassError.Style["color"] = "red";
                return false;
            }
            else if (txtPassword.Text.Trim() != txtPassword2.Text.Trim())
            {
                lblPass2Error.Text = "× 两次密码输入不一致，请重新输入！";
                lblPass2Error.Style["color"] = "red";
                return false;
            }
            else
            {
                lblPassError.Text = "√";
                lblPassError.Style["color"] = "white";
                lblPass2Error.Text = "√";
                lblPass2Error.Style["color"] = "white";
            }
            if (Regex.IsMatch(mail, @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+$") == false)
            {
                lblMailError.Text = "× 请输入正确的邮箱地址！";
                lblMailError.Style["color"] = "red";
                return false;
            }
            else if (count2 > 0)
            {
                lblMailError.Text = "× 邮箱地址已被注册！";
                lblMailError.Style["color"] = "red";
                return false;
            }
            else
            {
                lblMailError.Text = "√";
                lblMailError.Style["color"] = "white";
            }
            if (Regex.IsMatch(qq, @"^[1-9][0-9]{5,9}$") == false)
            {
                lblQqError.Text = "× 请输入正确的QQ号码！";
                lblQqError.Style["color"] = "red";
                return false;
            }
            else
            {
                lblQqError.Text = "√";
                lblQqError.Style["color"] = "white";
            }
            return true;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="uName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        private bool CreateUser(string uName, string password)
        {
           
            try
            {
                Random random = new Random();
                string salt = random.Next(1, 100).ToString();//注册数据数
                string pass_md5 = userMd5(password + salt);//MD5加密

                t_user u = new t_user();
                u.user_name = uName;//用户名
                u.user_pass = pass_md5;//密码
                u.reg_time = DateTime.Now;//注册时间
                u.user_salt = salt;//注册随机值
                u.last_login = DateTime.Now;//最后登录时间
                u.curr_ser = 0;//当前服务器
                u.email = txtmail.Text.Trim();  //邮箱地址
                u.qq = txtqq.Text.Trim();   //qq号
                
                DB.t_user.InsertOnSubmit(u); //插入玩家账号信息
                DB.SubmitChanges();//提交数据库修改

               

                t_testuser t = new t_testuser();//测试玩家列表
                var uid = from un in DB.t_user where un.user_name == uName select un.user_id;
                int[] qArray = uid.ToArray();
                t.user_id = qArray[0];
                t.user_name = uName;//玩家用户名
                string code = txtjihuoma.Text.Trim().ToLower();
                char[] flag = code.ToCharArray();
                int groupid = flag[6] - '0';//判断激活码最后一位，为玩家分组ID
                t.groupid = groupid;
                if (groupid == 5)
                { t.gifttimes = 1; }//非R玩家奖励次数为1次
                else
                {t.gifttimes = 10;};//其他玩家发放奖励次数为10次
                
                t.kodname = textkod.Text.Trim();//决斗之王用户名
                t.code = code; //账号注册所使用的激活码
                t.reg_time = DateTime.Now; //账号注册时间
                DB.t_testuser.InsertOnSubmit(t);


                var usedcode = DB.t_actcode.Where(r => r.actcode == txtjihuoma.Text.Trim().ToLower());//注册完成后删除使用过的激活码
                DB.t_actcode.DeleteAllOnSubmit(usedcode);


                DB.SubmitChanges();//提交数据库修改




                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// MD5　32位加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static string userMd5(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 

                pwd = pwd + s[i].ToString("X");

            }
            return pwd;
        }









    }
}