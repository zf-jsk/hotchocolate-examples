#

## Sample query:

```
mutation {
  loginUser(input: {  memberId: "123456789", password: "mypassword" }) {
  	access_token
    expires_in
  	membership {
      id
      firstName
      lastName
    }
  }
}
```
