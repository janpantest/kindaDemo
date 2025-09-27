package com.yourname.selenium;

import com.yourname.selenium.fixtures.TestFixture;
import com.yourname.selenium.pages.CartPage;
import com.yourname.selenium.pages.HomePage;
import com.yourname.selenium.pages.ProductsPage;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;
import org.openqa.selenium.WebDriver;

import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

public class UpdatedTest {

    TestFixture fixture;
    WebDriver driver;
    String username;
    String password;
    String baseUrl;
    String cartTitle;

    @Before
    public void setUp() {
        fixture = new TestFixture();
        driver = fixture.setUp(); // Initialize the WebDriver and perform setup
        username = fixture.getUsername();
        password = fixture.getPassword();
        baseUrl = fixture.getBaseUrl();
        cartTitle = fixture.getCartTitle();
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
        assertTrue("Cart buttons are visible", cartPage.areButtonsVisible());
        cartPage.removeFromCart();
        assertFalse("Badge is not visible", cartPage.cartBageNotVisible());
        cartPage.continueInShopping();

        assertTrue("Title of product should be visible", productsPage.isTitleVisible());
        productsPage.logout();

        assertTrue("Login form should be visible", homePage.isLoginFormVisible());
    }

    @After
    public void tearDown() {
        fixture.tearDown();
    }
}
