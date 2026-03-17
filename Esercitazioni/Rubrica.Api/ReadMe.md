TOKEN=$(curl -s -X POST "http://localhost:5036/api/Auth/login" \
    -H "Content-Type: application/json" \
    -d '{"email":"mario@email.com","password":"123456"}' | jq -r '.token')