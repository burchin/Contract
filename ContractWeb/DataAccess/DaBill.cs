﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

using ContractWeb.Models;
using ContractWeb.Common;
using ND.Lib.Data.SqlHelper;

namespace ContractWeb.DataAccess
{
    public class DaBill
    {
        /// <summary>
        /// 获取发票列表
        /// </summary>
        /// <returns></returns>
        public IList<Bill> getList()
        {
            string strSql = "select id, contractID, "
                + "(select z.name from ContractInfo z where z.id=contractID) as contractName, "
                + "(select z.money from ContractInfo z where z.id=contractID) as contractMoney, "
                + "type, (select z.name from BillType z where z.id=type) as typeName, money, date from Bill";

            IDataReader dr = SqlHelper.ExecuteReader(BaseHelper.DBConnStr, CommandType.Text, strSql);
            IList<Bill> list = DynamicBuilder<Bill>.ConvertToList(dr);
            return list;
        }

        /// <summary>
        /// 添加发票
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int add(Bill en)
        {
            string strSql = "insert into Bill (contractID, type, money, date) values (@contractID, @type, @money, @date)";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@contractID", en.contractID),
                new SqlParameter("@type", en.type),
                new SqlParameter("@money", en.money),
                new SqlParameter("@date", en.date)
            };

            int result = SqlHelper.ExecuteNonQuery(BaseHelper.DBConnStr, CommandType.Text, strSql, param);
            return result;
        }

    }
}