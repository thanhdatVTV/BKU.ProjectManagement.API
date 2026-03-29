# Local Deployment Script for BKU Project Management API

$VPS_ALIAS = "vps_bku_root"
$IMAGE_NAME = "bku-project-management-api"
$TAR_NAME = "bku-api.tar"

Write-Host "--- 1. Building Docker Image (Linux/amd64) ---" -ForegroundColor Cyan
# Using buildx to ensure compatibility with Linux VPS
docker buildx build --platform linux/amd64 -t $IMAGE_NAME . --load

Write-Host "--- 2. Exporting Image ---" -ForegroundColor Cyan
docker save -o $TAR_NAME $IMAGE_NAME

Write-Host "--- 3. Transferring files to VPS ---" -ForegroundColor Cyan
scp $TAR_NAME docker-compose.prod.yml nginx.conf deploy_vps.sh "$($VPS_ALIAS):~/"

Write-Host "--- 4. Executing remote deployment script ---" -ForegroundColor Cyan
ssh $VPS_ALIAS "sed -i 's/\r$//' deploy_vps.sh && chmod +x deploy_vps.sh && ./deploy_vps.sh"

Write-Host "--- 5. Cleanup local files ---" -ForegroundColor Cyan
Remove-Item $TAR_NAME

Write-Host "--- Done! ---" -ForegroundColor Green
