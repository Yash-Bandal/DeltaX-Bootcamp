### Dependency Injection Shortcut

0. Define Interface
   ```csharp
    public interface IDatabase
    {
        public void Save();
    }
   ```
2. Prews `ctor` and declare default empty constructor
    ```
    ctor
    ```
    ```csharp
    public OrderService()
    {
              
    }
    ```

3. Now inside paramter field Type Interface eg `IDatabase database`
    ```csharp
    public OrderService(IDatabase databsase)
    {
              
    }
    ```
    <img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/930ecbec-4d9e-4472-9b6c-f6780859b812" />

4. Press `alt + Enter` or `ctrl + .` and select `Create and assign field database`
    ```csharp
    public OrderService(IDatabase database)
    {
       this.database = database;
    }
    ```

   <img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/74612920-6b6e-4894-b274-43893967ede7" />
