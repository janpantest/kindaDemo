from playwright.sync_api import Page
from helpers.helper import click_if_visible

class IceHomePage:

    def __init__(self, page: Page):
        self.page = page
        self.header = page.locator('div.logoResponsive')
        self.user_drop_down = page.locator('.dropdown.user.user-menu')
        self.my_info_link = page.get_by_role('link', name='Profile')
        self.cancel_button = page.locator('button.cancelBtn')
    
    def check_header(self):
        self.header.is_visible()

    def clickIfElementExists(self):
        click_if_visible(self.cancel_button)

    def go_to_my_info(self):
        # self.page.wait_for_timeout(5000)
        click_if_visible(self.cancel_button)
        self.user_drop_down.is_visible()
        self.user_drop_down.click()
        self.my_info_link.is_visible()
        self.my_info_link.click()
        
    def get_user(self):
        # self.header_h4.is_visible()
        text = self.header.inner_text()
        print(text)
        return str(text)
