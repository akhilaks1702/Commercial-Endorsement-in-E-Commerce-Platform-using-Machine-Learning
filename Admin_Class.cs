using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DATAMINING_ASSOCIATIONRULE.DataLayerTableAdapters;

namespace DATAMINING_ASSOCIATIONRULE
{
    public class Admin_Class
    {
        Admin_DetailsTableAdapter admin_obj = new Admin_DetailsTableAdapter();
        Item_CategoryTableAdapter category_obj = new Item_CategoryTableAdapter();
        Item_SubCategoryTableAdapter subcategory_obj = new Item_SubCategoryTableAdapter();
        Item_DetailsTableAdapter item_obj = new Item_DetailsTableAdapter();
        DataTable1TableAdapter dt1_obj = new DataTable1TableAdapter();
        Customer_DetailsTableAdapter cust_obj = new Customer_DetailsTableAdapter();
        Customer_TransactionsTableAdapter custtrans_obj = new Customer_TransactionsTableAdapter();
        Transaction_DetailsTableAdapter trans_obj = new Transaction_DetailsTableAdapter();
        FeedbacksTableAdapter feedback_obj = new FeedbacksTableAdapter();
        DataTable2TableAdapter dt2_obj = new DataTable2TableAdapter();
        RatingsTableAdapter ratingObj = new RatingsTableAdapter();

        tblAdsTableAdapter adsObj = new tblAdsTableAdapter();

        #region ----------ADMIN CHANGEPASSWORD ---------

        public void AdminChangepassword(string password, string AdminID)
        {
            admin_obj.ChangeAdminPassword(password, AdminID);
        }

        public DataTable GetAdminDetails(string AdminID)
        {
            return admin_obj.GetAdminDetails(AdminID);
        }


        #endregion

        #region ------ MANAGE ITEM CATEGORIES ---------

        public void NewItemCategory(string category)
        {
            category_obj.NewCategory(category);
        }

        public void DeleteItemCategory(int category_ID)
        {
            category_obj.DeleteCategory(category_ID);
        }

        public DataTable GetAllCategories()
        {
            return category_obj.GetData();
        }

        public bool CheckItemCategory(string Category)
        {
            int cnt = int.Parse(category_obj.CheckItemCategory(Category).ToString());

            if (cnt == 1)

                return false;

            else

                return true;
        }

        public DataTable GetCategoryDetails(int categoryID)
        {
            return category_obj.GetCategoryDetails(categoryID);
        }

        #endregion

        #region --------- MANAGE ITEM SUBCATEGORIES -------

        public void NewItemSubCategory(int categoryID, string subcategory, string description)
        {
            subcategory_obj.NewSubCategory(categoryID, subcategory, description);
        }

        public void UpdateItemSubCategoryDetails(int categoryID, string subcategory, string description, int subcategoryID)
        {
            subcategory_obj.UpdateSubCategoryDetails(categoryID, subcategory, description, subcategoryID);
        }

        public void DeleteItemSubCategory(int subcategoryID)
        {
            subcategory_obj.DeleteSubCategory(subcategoryID);
        }

        public void DeleteCategorySubCategories(int categoryID)
        {
            subcategory_obj.DeleteCategorySubCategories(categoryID);
        }

        public DataTable GetAllSubCategories()
        {
            return subcategory_obj.GetData();
        }

        public DataTable GetCategorySubCategories(int categoryID)
        {
            return subcategory_obj.GetCategorySubCategories(categoryID);
        }

        public bool CheckCategorySubCategory(int categoryID, string subcategory)
        {
            int cnt = int.Parse(subcategory_obj.CheckCategorySubCategory(categoryID, subcategory).ToString());

            if (cnt == 1)

                return false;

            else

                return true;
        }

        public DataTable GetSubCategoryDetails(int SubcategoryID)
        {
            return subcategory_obj.GetSubCategoryDetails(SubcategoryID);
        }

        #endregion

        #region --------- MANAGE ITEMS ---------

        public void NewItem(int subcategoryID, string Item, string ItemDetails, double ItemCost, string ItemImage, int Quantity, string otherdetails)
        {
            item_obj.InsertItem(subcategoryID, Item, ItemCost, ItemDetails, ItemImage, Quantity,  otherdetails);
        }

        public void UpdateItemDetails(int subcategoryID, string Item, string ItemDetails, double ItemCost, string ItemImage, int Quantity,  string otherdetails, int ItemID)
        {
            item_obj.UpdateItemDetails(subcategoryID, Item, ItemCost, ItemDetails, ItemImage, Quantity,  otherdetails, ItemID);
        }

        public void DeleteItem(int ItemID)
        {
            item_obj.DeleteItem(ItemID);
        }

        public void DeleteSubCategoryItems(int subcategoryID)
        {
            item_obj.DeleteSubCategoryItems(subcategoryID);
        }

        public DataTable GetAllItems()
        {
            return item_obj.GetData();
        }

        public DataTable GetSubcategoryItems(int subcategoryID)
        {
            return item_obj.GetSubCategoryItems(subcategoryID);
        }

        public bool CheckSubCategoryItem(int subcategory, string Item)
        {
            int cnt = int.Parse(item_obj.CheckSubCategoryItem(subcategory, Item).ToString());

            if (cnt == 1)

                return false;

            else

                return true;
        }

        public DataTable GetItemDetails(int ItemID)
        {
            return item_obj.GetItemDetails(ItemID);
        }

        public DataTable GetCategoryItems(int categoryID)
        {
            return dt1_obj.GetCategoryItems(categoryID);
        }

        public DataTable GetItems_Categ_SubCategID(int categoryID, int subcategoryID)
        {
            return dt1_obj.GetItems_Categ_SubCateg(categoryID, subcategoryID);
        }

        public bool CheckItemName(string ItemName)
        {
            int cnt = int.Parse(item_obj.CheckItemName(ItemName).ToString());

            if (cnt == 1)

                return false;

            else

                return true;
        }

        //function to get the item rating
        public int GetItemRating(int itemId)
        {
            return (int)ratingObj.GetItemRating(itemId);
        }

        //function to check the item rating
        public DataTable GetRatingByItem(int itemId)
        {
            return ratingObj.GetRatingByItem(itemId);
        }


        #endregion

        #region -------- MANAGE CUSTOMER TRANSACTIONS -------

        public DataTable GetAllCustomers()
        {
            return cust_obj.GetData();
        }

        public void DeleteCustomer(string emailId)
        {
            cust_obj.DeleteCustomer(emailId);
        }

        public void DeleteCustomerTransactions(string emailId)
        {
            custtrans_obj.DeleteCustomerTransactions(emailId);
        }

        public DataTable GetAllCustomerTransactions()
        {
            return custtrans_obj.GetData();
        }

        public DataTable GetTransactionsBasedonStatus(string status)
        {
            return custtrans_obj.GetAllTransactionsBasedonStatus(status);
        }

        public DataTable GetTransactionDetails(int TransactionID)
        {
            return custtrans_obj.GetTransactionDetails(TransactionID);
        }

        public void UpdateCustomerTransaction(string dispatcheddate, int transactionID)
        {
            custtrans_obj.UpdateCustomerTransaction(dispatcheddate, transactionID);
        }

        public DataTable GetTransactionDetails_ID(int transactionID)
        {
            return trans_obj.GetTransactionDetails(transactionID);
        }

        public DataTable GetCustomerDetails(string EmailID)
        {
            return cust_obj.GetCustomerDetails(EmailID);
        }

        public void DeleteTransactionItems(int TransactionId)
        {
            trans_obj.DeleteTransactionItems(TransactionId);
        }

        public void DeleteTransactionDetails(int DetailsId)
        {
            trans_obj.DeleteTransactionDetails(DetailsId);
        }

        public void DeleteTransaction(int transactionId)
        {
            custtrans_obj.DeleteTransaction(transactionId);
        }

        #endregion

        #region ----------- MANAGE FEEDBACKS ----------------

        public void DeleteFeedback(int feedbackID)
        {
            feedback_obj.DeleteFeedback(feedbackID);
        }

        public DataTable GetAllFeedbacks()
        {
            return feedback_obj.GetData();
        }


        #endregion

        public DataTable GetAllAds()
        {
            return adsObj.GetData();

        }

        public DataTable GetAllActiveAds()
        {
            return adsObj.GetAllActiveAds();

        }

        public DataTable GetAdsBySubCId(int subId)
        {
            return adsObj.GetAdsBySubCId(subId);

        }

        public void InsertAd(int subcategory, string offer, string details, DateTime date, string status)
        {
            adsObj.InsertAd(subcategory, offer, details, date, status);
        }

        public void UpdateAd(int subcategory, string offer, string details, DateTime date, string status, int adId)
        {
            adsObj.UpdateAd(subcategory, offer, details, date, status, adId);
        }

        public void DeleteAd(int adId)
        {
            adsObj.DeleteAds(adId);
        }

        public DataTable GetAdById(int adId)
        {
            return adsObj.GetAdById(adId);
        }
    }
}
