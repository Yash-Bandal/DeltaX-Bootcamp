# Common Patterms
## 1. Generalization / Enumeration of category

<div align = "center">
<img width="500" alt="image" src="https://github.com/user-attachments/assets/0f4c425b-251f-4747-87fc-fb0952f91d05" />
</div>

You can use for any types  like :-
- User types (user_category - customer, rider)
- Product types (product_category - electronics, grocery , etc)
- Account types (account_type - Savings, current)

<br>

## 2. Self-Referencing Relationship

<div align = "center">
  <img width="350" alt="image" src="https://github.com/user-attachments/assets/f60c8ea1-af0e-441a-8e2c-df4468b18f35" />
  <img width="200" alt="image" src="https://github.com/user-attachments/assets/60cf9651-ee51-4614-bf3b-d0636f06cf75" />
</div>

- YouTube - Channel_profile

    Columns
    ```
    Subscribed_By
    Subscribed_To
    ```
  
-  Instagram
 
    User -> Follow -> User
    
    Columns
    ```
    Follower_ID
    Following_ID
    ```

-  Facebook

    User -> Friend -> User

- Ratings
  One user/business -> Rate another user/business (same user table)

   Columns
   ```
   from_user_id
   to_user_id
   ```
  
