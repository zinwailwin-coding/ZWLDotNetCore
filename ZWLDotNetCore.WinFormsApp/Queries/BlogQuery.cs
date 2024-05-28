using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWLDotNetCore.WinFormsApp.Queries
{
    public class BlogQuery
    {
        public static string BlogCreate = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

        public static string BlogList = @"Select [BlogId]
           ,[BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent] From [dbo].[Tbl_Blog]";
    }
}
