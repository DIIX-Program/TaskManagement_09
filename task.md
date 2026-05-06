Danh sách công việc (TODO List)
1. Cập nhật API (Backend)
 Tạo thực thể Report cho việc báo cáo giữa Employee và Manager.
 Cập nhật ApplicationDbContext và chạy Migration.
 Cập nhật ProjectDtos thêm thuộc tính Progress.
 Cập nhật ProjectService:
 Thêm logic tính % tiến độ.
 Cập nhật logic GetAllProjectsAsync theo vai trò.
 Thêm kiểm tra quyền cho UpdateProjectAsync và AddMemberAsync.
 Cập nhật TaskService:
 Kiểm tra quyền thành viên dự án trước khi tạo Task.
 Cập nhật NotificationService:
 Thêm phương thức MarkAsRead.
 Thêm phương thức gửi thông báo nhóm.
 Cập nhật ReportsController API:
 RBAC (Phân quyền): Hạn chế quyền hạn của Manager/Employee dựa trên dự án họ tham gia.
 Tính năng Báo cáo/Tin nhắn: Triển khai ReportService và giao diện tin nhắn.
 Cải thiện giao diện (Web UI):
 Sửa lỗi bộ lọc, tìm kiếm tại các trang Dự án, Đội ngũ.
 Thay đổi "Chỉ số sức khỏe" thành "Tiến độ dự án" và hiển thị % hoàn thành.
 Sửa nút "Tạo công việc đầu tiên" trong chi tiết dự án.
 Tính năng mời nhân viên qua Email (Team management).
2. Cập nhật Web (Frontend)
 Cập nhật trang Projects/Index: Thêm tìm kiếm và lọc trạng thái.
 Cập nhật trang Projects/Details:
 Đổi "Chỉ số sức khỏe" thành "Tiến độ dự án".
 Hiển thị % tiến độ thực tế.
 Sửa nút "Tạo công việc đầu tiên".
 Phân quyền hiển thị nút Sửa/Xóa/Thêm thành viên.
 Cập nhật trang Team/Index (Our Partners):
 Sửa bộ lọc và tìm kiếm.
 Thêm tính năng gửi thông báo.
 Cập nhật trang Tasks/Index: Sửa bộ lọc và tìm kiếm.
 Cập nhật trang Reports/Index:
 Thêm chọn dự án và nút xuất báo cáo.
 Thêm bảng vinh danh nhân viên.
 Cập nhật trang Register: Hiển thị thông báo thành công và chờ phê duyệt.
 Cập nhật trang Settings: Cấu hình thông báo.
3. Hoàn thiện & Sửa lỗi
 Sửa lỗi "Entity changes" (nếu phát hiện nguyên nhân).
 Cập nhật nội dung cho các trang Landing Page (Privacy, Terms, ...).
