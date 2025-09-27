package com.yourname.selenium;

import com.yourname.selenium.pages.CartPage;
import com.yourname.selenium.pages.HomePage;
import com.yourname.selenium.pages.ProductsPage;

import io.github.bonigarcia.wdm.WebDriverManager;
import io.github.cdimascio.dotenv.Dotenv;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.edge.EdgeDriver;

import static org.junit.Assert.assertTrue;

public class AppTest {

    WebDriver driver;
    String username;
    String password;
    String baseUrl;
    String cartTitle;

    @Before
    public void setUp() {
        String url = "https://www.saucedemo.com";

        // System.setProperty("webdriver.chrome.driver", "path/to/chromedriver");
        WebDriverManager.chromedriver().setup();

        // Create a new instance of Chrome driver
        driver = new FirefoxDriver();
        // driver = new ChromeDriver();
        // driver = new EdgeDriver();
        driver.manage().window().maximize();
        driver.get(url);

        // Dotenv dotenv = Dotenv.load();
        Dotenv dotenv = Dotenv.configure().load(); 

        baseUrl = dotenv.get("BASE_URL");
        username = dotenv.get("USER_NAME");
        password = dotenv.get("PASSWORD");
        cartTitle = "Your Cart";
    }

    @Test
    public void testLoginWorkflow() {
        HomePage homePage = new HomePage(driver);
        ProductsPage productsPage = new ProductsPage(driver);
        CartPage cartPage = new CartPage(driver);

        // Check if login form is visible
        assertTrue("Login form should be visible", homePage.isLoginFormVisible());
        homePage.login(username, password);

        assertTrue("Title of product should be visible", productsPage.isTitleVisible());
        productsPage.addFirstProductToBasket();
        productsPage.isRemoveButtonVisible();
        assertTrue("Remove button should be visible", productsPage.isCartBadgeVisible());
        String product = productsPage.getProductText();
        productsPage.goToCart();

        cartPage.checkCartTitle(cartTitle);
        cartPage.checkProductName(product);
    }

    @After
    public void tearDown() {
        if (driver != null) {
            driver.quit();
        }
    }
}
