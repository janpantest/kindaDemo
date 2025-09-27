package com.yourname.selenium.fixtures;

import io.github.bonigarcia.wdm.WebDriverManager;
import io.github.cdimascio.dotenv.Dotenv;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.firefox.FirefoxDriver;

public class TestFixture {

    WebDriver driver;
    String username;
    String password;
    String baseUrl;
    String cartTitle;

    // Method for initializing the WebDriver and other setup tasks
    public WebDriver setUp() {
        String url = "https://www.saucedemo.com";

        // WebDriverManager.edgedriver().setup(); // Manage WebDriver binary for Edge
        WebDriverManager.chromedriver().setup();
        // driver = new EdgeDriver();
        driver = new FirefoxDriver();
        driver.manage().window().maximize();
        driver.get(url);

        Dotenv dotenv = Dotenv.configure().load(); 
        baseUrl = dotenv.get("BASE_URL");
        username = dotenv.get("USER_NAME");
        password = dotenv.get("PASSWORD");
        cartTitle = "Your Cart";

        return driver;
    }

    // Cleanup method to quit the driver after tests
    public void tearDown() {
        if (driver != null) {
            driver.quit();
        }
    }

    // Getter methods for easy access in tests
    public WebDriver getDriver() {
        return driver;
    }
    
    public String getUsername() {
        return username;
    }

    public String getPassword() {
        return password;
    }

    public String getBaseUrl() {
        return baseUrl;
    }

    public String getCartTitle() {
        return cartTitle;
    }
}
