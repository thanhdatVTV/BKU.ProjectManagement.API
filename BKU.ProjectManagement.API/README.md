# BKU Project Management API

Hệ thống quản lý dự án BKU được xây dựng trên nền tảng .NET 8, hỗ trợ Docker hóa hoàn toàn cho cả môi trường phát triển (Local) và chính thức (Production).

## 🚀 Quick Start (Local)

Để chạy dự án dưới máy local mà không cần cài đặt SQL Server hay .NET SDK:

1.  **Yêu cầu**: Cài đặt [Docker Desktop](https://www.docker.com/products/docker-desktop/).
2.  **Khởi chạy**: Mở terminal tại thư mục gốc và chạy:
    ```powershell
    docker-compose up --build
    ```
3.  **Truy cập**:
    - **Swagger UI (qua Nginx)**: [http://localhost/swagger](http://localhost/swagger)
    - **API Trực tiếp**: [http://localhost:8080/swagger](http://localhost:8080/swagger)

> [!TIP]
> Hệ thống tích hợp cơ chế **Auto-migration**, database sẽ tự động được khởi tạo khi container `api` khởi chạy lần đầu.

---

## 🌐 Deployment (VPS)

Dự án hỗ trợ quy trình deploy tự động: "Build locally -> Transfer -> Run on VPS".

### 1. Cấu hình SSH
Đảm bảo bạn đã có alias SSH trong file `~/.ssh/config` tên là `vps_bku_root`.

### 2. Chạy lệnh Deploy
Từ máy local (Windows PowerShell), chạy lệnh duy nhất:
```powershell
.\deploy.ps1
```
Script này sẽ xử lý:
- Build image chuẩn Linux/amd64.
- Nén và gửi image cùng cấu hình lên VPS.
- Tự động load image và khởi chạy container trên server.

---

## 🛠 Cấu trúc dự án
- **BKU.ProjectManagement.API**: Lớp API (Controller, Middleware).
- **BKU.ProjectManagement.Repositories**: Lớp dữ liệu (EF Core, Migrations, Entities).
- **BKU.ProjectManagement.Services**: Lớp nghiệp vụ (Business Logic).
- **BKU.ProjectManagement.Shared**: Các model và base class dùng chung.

---

## ⚙️ Biến môi trường quan trọng
Bạn có thể ghi đè các giá trị này trong `docker-compose.yml` hoặc `docker-compose.prod.yml`:

- `ASPNETCORE_ENVIRONMENT`: `Development` hoặc `Production`.
- `ConnectionStrings__BKUConnection`: Connection string tới SQL Server.
- `ASPNETCORE_HTTP_PORTS`: Cấu hình port (mặc định 8080).

---

## 🔒 Bảo mật & Lưu ý
- **HTTPS**: Hiện tại dự án đang chạy HTTP port 80 (qua Nginx). Để cấu hình SSL (Certbot), hãy cập nhật `nginx.conf`.
- **Password**: Hãy thay đổi mật khẩu mặc định của SQL Server (`YourStrong!Passw0rd`) trong các file docker-compose trước khi deploy thực tế.
