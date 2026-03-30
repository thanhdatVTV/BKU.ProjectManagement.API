#!/bin/bash
set -e

echo "=== Step 1: Import Microsoft GPG key ==="
curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg 2>/dev/null || true

echo "=== Step 2: Add SQL Server 2022 repository ==="
echo "deb [arch=amd64 signed-by=/usr/share/keyrings/microsoft-prod.gpg] https://packages.microsoft.com/ubuntu/22.04/mssql-server-2022 jammy main" | sudo tee /etc/apt/sources.list.d/mssql-server-2022.list > /dev/null

echo "=== Step 3: Update package list ==="
sudo apt-get update -y

echo "=== Step 4: Install SQL Server ==="
sudo apt-get install -y mssql-server

echo "=== Step 5: Configure SQL Server (Developer Edition) ==="
sudo ACCEPT_EULA=Y MSSQL_SA_PASSWORD='Hanh@2205' MSSQL_PID=Developer /opt/mssql/bin/mssql-conf setup || true

echo "=== Step 6: Check SQL Server status ==="
sudo systemctl status mssql-server --no-pager

echo "=== Step 7: Install SQL Server tools ==="
curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo gpg --dearmor -o /usr/share/keyrings/microsoft-prod-tools.gpg 2>/dev/null || true
echo "deb [arch=amd64 signed-by=/usr/share/keyrings/microsoft-prod-tools.gpg] https://packages.microsoft.com/ubuntu/22.04/prod jammy main" | sudo tee /etc/apt/sources.list.d/mssql-tools.list > /dev/null
sudo apt-get update -y
sudo ACCEPT_EULA=Y apt-get install -y mssql-tools18 unixodbc-dev

echo "=== Step 8: Add sqlcmd to PATH ==="
echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bashrc

echo "=== Step 9: Test SQL Server connection ==="
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P 'Hanh@2205' -C -Q "SELECT @@VERSION" || echo "WARNING: sqlcmd test failed, but SQL Server may still be starting up"

echo "=== Step 10: Create database ==="
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P 'Hanh@2205' -C -Q "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'BKU_SPMS_DB') CREATE DATABASE BKU_SPMS_DB" || echo "WARNING: Database creation may need retry"

echo "=== SQL Server Installation Complete! ==="
