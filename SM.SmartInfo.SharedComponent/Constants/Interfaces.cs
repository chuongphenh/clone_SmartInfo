using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params;
using System.Web.UI.WebControls;
namespace SM.SmartInfo.SharedComponent.Constants
{
    #region Forms

    /// <summary>
    /// Interface chung cho tất cả các form
    /// </summary>
    public interface ISMForm
    {
        /// <summary>
        /// Chuẩn bị toàn bộ dữ liệu cho form khi Load (ComboBox, Dữ liệu cần hiển thị,...)
        /// </summary>
        void SetupForm();
    }

    /// <summary>
    /// Interface cho form AddNew
    /// </summary>
    /// <typeparam name="T">Entity của bảng sẽ xử lý</typeparam>
    public interface ISMFormAddNew<T> : ISMForm
    {
        /// <summary>
        /// Bind data từ Form vào Item.
        /// </summary>
        /// <returns></returns>
        T BindFormToObject();

        /// <summary>
        /// Thêm Item mới vào Database
        /// </summary>
        /// <returns>ID Item vừa tạo để redirect sang trang Display</returns>
        object AddNewItem();
    }

    /// <summary>
    /// Interface cho form Display
    /// </summary>
    /// <typeparam name="T">Entity của bảng sẽ xử lý</typeparam>
    public interface ISMFormDisplay<T> : ISMForm
    {
        //void LoadData();

        /// <summary>
        /// Bind data từ Item lên form
        /// </summary>
        /// <param name="item"></param>
        void BindObjectToForm(T item);

        /// <summary>
        /// Xóa Item đang hiển thị khỏi Database. Thường sẽ redirect về trang View
        /// </summary>
        void DeleteItems();   //Remove. Gop chung DeleteItems(T param)
    }

    /// <summary>
    /// Interface cho form Edit
    /// </summary>
    /// <typeparam name="T">Entity của bảng sẽ xử lý</typeparam>
    public interface ISMFormEdit<T> : ISMForm
    {
        //void LoadData();

        /// <summary>
        /// Bind data từ Item lên form
        /// </summary>
        /// <param name="item"></param>
        void BindObjectToForm(T item);

        /// <summary>
        /// Bind data từ Form vào Item.
        /// </summary>
        /// <returns></returns>
        T BindFormToObject();

        /// <summary>
        /// Bind data từ Form vào Item. Update Item vào DatabaseUpdate. Thường sẽ redirect về trang Display
        /// </summary>
        void UpdateItem();
    }

    /// <summary>
    /// Interface cho việc xử lý Grid, hỗ trợ paging.
    /// Hạn chế dùng. Dùng ISMFormView
    /// </summary>
    /// <typeparam name="T">Entity của bảng sẽ xử lý</typeparam>
    public interface ISMFormGridView<T> : ISMForm // Bo, Gop chung voi ISMFormDefault
    {
        /// <summary>
        /// Bind dữ liệu từ object vào GridItem để hiển thị
        /// </summary>
        void BindObjectToGridItem(WebControl gridItem);
        /// <summary>
        /// Paging. Lấy dữ liệu từ Database dựa vào trang được chọn và bind lên Grid.
        /// </summary>
        void SearchItemsForView(); // Doi -> Search
    }

    /// <summary>
    /// Interface cho form View hỗ trợ Xóa
    /// </summary>
    /// <typeparam name="T">Entity của bảng sẽ xử lý</typeparam>
    public interface ISMFormDefault<T> : ISMForm, ISMFormGridView<T>
    {
        /// <summary>
        ///  Bind dữ liệu từ GridItem vào object, dùng khi Delete
        /// </summary>
        T BindGridItemToObject(WebControl gridItem);
        /// <summary>
        /// Xóa 1 hoặc nhiều Item được chọn khỏi Database
        /// </summary>
        /// <param name="data">Có thể là GridItem nếu xóa 1, Grid nếu xóa nhiều</param>
        void DeleteItems();
    }

    #endregion

    #region Biz
    /// <summary>
    /// Business cho Form AddNew
    /// </summary>
    /// <typeparam name="T">Entity của Param</typeparam>
    public interface ISMFormAddNewBiz<T>
    {
        /// <summary>
        /// Lấy các dữ liệu cần thiết để hiển thị lên form AddNew khi Load (Dữ liệu cho Combobox,...)
        /// </summary>
        /// <param name="param">Kiểu dữ liệu của param sẽ được định nghĩa khi 1 class implement interface</param>
        void SetupAddNewForm(T param); //Remove. Gop chung voi Edit -> SetupAddNewEditForm
        /// <summary>
        /// Thêm Item được truyền xuống vào Database
        /// </summary>
        /// <param name="param">Kiểu dữ liệu của param sẽ được định nghĩa khi 1 class implement interface</param>
        void AddNewItem(T param);
        /// <summary>
        /// Validate dữ liệu truyền xuống từ IU.
        /// ID: Null -> Validate add new
        /// </summary>
        /// <param name="param"></param>
        void ValidateItem(T param);     //ValidateItem(T param, bool isAddNew)
    }

    /// <summary>
    /// Business cho Form Display
    /// </summary>
    /// <typeparam name="T">Entity của Param</typeparam>
    public interface ISMFormDisplayBiz<T>
    {
        /// <summary>
        /// Lấy các dữ liệu cần thiết để hiển thị lên form Display khi Load (Hầu như không có)
        /// </summary>
        /// <param name="param">Kiểu dữ liệu của param sẽ được định nghĩa khi 1 class implement interface</param>
        //void SetupDisplayForm(T param);// Remove. Khong thay ham nao co code
        /// <summary>
        /// Dữ liệu của Item cần hiển thị
        /// </summary>
        /// <param name="param"></param>
        void LoadDataDisplay(T param);
        /// <summary>
        /// Xóa Item được truyền xuống khỏi Database
        /// </summary>
        /// <param name="param">Kiểu dữ liệu của param sẽ được định nghĩa khi 1 class implement interface</param>
        void DeleteItems(T param); //Remove. Doi ten de dung chung voi view
    }

    /// <summary>
    /// Business cho Form Edit
    /// </summary>
    /// <typeparam name="T">Entity của Param</typeparam>
    public interface ISMFormEditBiz<T>
    {
        /// <summary>
        /// Lấy các dữ liệu cần thiết để hiển thị lên form Edit khi Load (Dữ liệu cho Combobox,...)
        /// </summary>
        /// <param name="param">Kiểu dữ liệu của param sẽ được định nghĩa khi 1 class implement interface</param>
        void SetupEditForm(T param);
        /// <summary>
        /// Lấy dữ liệu của Item cần hiển thị
        /// </summary>
        /// <param name="param"></param>
        void LoadDataEdit(T param);
        /// <summary>
        /// Cập nhật Item được truyền xuống vào Database
        /// </summary>
        /// <param name="param">Kiểu dữ liệu của param sẽ được định nghĩa khi 1 class implement interface</param>
        void UpdateItem(T param);
        /// <summary>
        /// Validate dữ liệu truyền xuống từ IU
        /// ID: Not null -> Validate edit/update
        /// </summary>
        /// <param name="param"></param>
        void ValidateItem(T param);
    }

    /// <summary>
    /// Business cho Form View
    /// </summary>
    /// <typeparam name="T">Entity của Param</typeparam>
    public interface ISMFormDefaultBiz<T>
    {
        /// <summary>
        /// Lấy các dữ liệu cần thiết để hiển thị lên form View khi Load (Dữ liệu cho Combobox, Dữ liệu của các Item cần hiển thị ...)
        /// </summary>
        /// <param name="param">Kiểu dữ liệu của param sẽ được định nghĩa khi 1 class implement interface</param>
        void SetupViewForm(T param);
        /// <summary>
        /// Xóa các Item được truyền xuống khỏi Database.
        /// Các Item này thường từ nút xóa trên từng Row, hoặc khi chọn nhiều Items qua CheckBox rồi xóa hàng loạt.
        /// </summary>
        /// <param name="param">Kiểu dữ liệu của param sẽ được định nghĩa khi 1 class implement interface</param>
        void DeleteItems(T param);
        /// <summary>
        /// Lấy các Item từ Database để hiển thị lên view dựa vào số trang được truyền xuống
        /// </summary>
        /// <param name="param">Kiểu dữ liệu của param sẽ được định nghĩa khi 1 class implement interface</param>
        void SearchItemsForView(T param);
    }

    /// <summary>
    /// Interface cho Biz của chức năng CRUD.
    /// </summary>
    /// <typeparam name="T">Entity của Param</typeparam>
    public interface ISMFormCRUDBiz<T> : ISMFormAddNewBiz<T>, ISMFormDisplayBiz<T>, ISMFormEditBiz<T>, ISMFormDefaultBiz<T>
    {
    }
    #endregion
}
