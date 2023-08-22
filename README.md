# Garage-Finder
## Introduction
Đây là 1 project đồ án làm về 1 hệ thống gợi ý chỗ bão dưỡng, sửa xe.  \
Đây là trang chủ của web: https://garagefinder.me/  \
Đây là link api: https://garage-finder-2.azurewebsites.net/swagger/index.html  \
Frontend: react  \
Backend: .net 6  \
Database: microsoft sql server
## Architecture
![Architechture drawio](https://github.com/vinhdn20/Garage-Finder/assets/74886989/c5dc8a9d-94a8-4b68-a774-6375b4f0dc58)
- Websocket: Backend sử dụng thư viện System.Net.WebSockets để xử chức năng realtime.
- VNPAY: dùng cho chức năng thanh toán. Dùng thẻ sau đây để thử chức năng thanh 
- Twillio: để gửi tin nhắn xác thực đến sdt người dùng
- Mailjet: gửi mail đến cho người dùng
- Azure Blob: lưu ảnh và các file khác
- Azure repos: dùng để lưu source code
- Azure app service: để deploy api BE
- Vercel: deploy front end
- CI/CD: Azure pipeline, vercel
## Project management
- Dự án này được phát triển dựa trên mô hình scrum, dùng công cụ azure devops để phân chia task, quản lý sprint,...
## Deploy front end
Installation for Website
- Free Solution: In this part, we will demo deploy on Vercel
  - Access to Vercel
  - Connect a Git Provider
  - Choose repository which contains source code of Garage Finder Web
  - Setup following in Environment Variables of project:
    - NEXT_PUBLIC_BASE_URL: Put API URL in this field (Example:
https://api.[yourdomain])
    - NEXT_PUBLIC_CLIENT_ID: Put Google OAuth 2.0 Client IDs in this field
  - Click to Deploy
  - Done
- Paid Solution: In this part, we will demo deploy on Digital Ocean
  - Access to Digital Ocean
  - Create an app
  - Choose repository which contains source code of Garage Finder Web
  - Choose a proper hardware plan
  - Add Environment Variables the same as free solution above
  - Click Create Resources
  - Done
## Deploy backend
- Access to portal Azure
- Create an app service
- Create a SQL server
- Create a storage account
- Download visual studio 2022: link
- Create a Mailjet account and config API: link
- Create a Twilio account
- Create a VNPay account
- Config appsettings.json:
  - Access file appsettings.json
  - Config ConnectionStrings to azure sql database
  - Config Twilio: AccountSID, AuthToken, VerificationServiceSID
  - Config Azure to connect to a storage account
  - Config Mailjet and VNPay
- Deploy database
  - Open source
  - access to Package manager console window
  - type command: $env:ASPNETCORE_ENVIRONMENT='production'
  - type command: update-database
  - Run script DBScript/Garage-Finder.sql
- Deploy API
  - Access to dev Azure and create a project
  - Publish source to dev Azure
  - Create a pipeline and connect to app service
  - Run pipeline and deploy to app service
- Done
## Other
- Dùng thẻ này để test chức năng thanh toán
  > Ngân hàng: NCB
  > Số thẻ: 9704198526191432198
  > Tên chủ thẻ:NGUYEN VAN A
  > Ngày phát hành:07/15
  > Mật khẩu OTP:123456
- Account admin:
  > admin@gmail.com
  > 123
