docker run -p 5555:80 `
  --name pgadmin_localhost `
  -e "PGADMIN_DEFAULT_EMAIL=test@test.com" `
  -e "PGADMIN_DEFAULT_PASSWORD=test" `
  dpage/pgadmin4
