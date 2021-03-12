using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Application.ElasticSearchServices.Settings
{
    public static class ElasticSearchIndexDocumentNames
    {
        public static string ProductIndexName = "productevent"; 
        public static string ProductIndexAliasName = "productevent_history"; 
        public static string ProductDocumentPositionIndexName = "productpointdata"; 
        public static string ProductDocumentPositionAliasName = "productpoint_history"; 
        public static string ProductDocumentPositionName = "productposition";

        public static string OrderIndexName = "orderevent";
        public static string OrderIndexAliasName = "orderevent_history";
        public static string OrderDocumentPositionIndexName = "orderpointdata";
        public static string OrderDocumentPositionAliasName = "orderpoint_history";
        public static string OrderDocumentPositionName = "orderposition";

        public static string EmployeeIndexName = "employeeevent";
        public static string EmployeeIndexAliasName = "employeeevent_history";
        public static string EmployeeDocumentPositionIndexName = "employeepointdata";
        public static string EmployeeDocumentPositionAliasName = "employeepoint_history";
        public static string EmployeeDocumentPositionName = "employeeposition";

        public static string CustomerIndexName = "customerevent";
        public static string CustomerIndexAliasName = "customerevent_history";
        public static string CustomerDocumentPositionIndexName = "customerpointdata";
        public static string CustomerDocumentPositionAliasName = "customerpoint_history";
        public static string CustomerDocumentPositionName = "customerposition";

        public static string SupplierIndexName = "supplierevent";
        public static string SupplierIndexAliasName = "supplierevent_history";
        public static string SupplierDocumentPositionIndexName = "supplierpointdata";
        public static string SupplierDocumentPositionAliasName = "supplierpoint_history";
        public static string SupplierDocumentPositionName = "supplierposition";
    }
}
