from playwright.sync_api import Page

class IceLoginPage:

    def __init__(self, page: Page):
        self.page = page
        self.user_name_input = page.get_by_role('textbox', name='Email or Username Email or')
        self.password_input = page.get_by_role('textbox', name='Password')
        self.login_button = page.get_by_role('button', name='Log in')
    
    def go_to(self, url: str):
        self.page.goto(url)
    
    def check_login_elements(self):
        self.user_name_input.is_visible()
        self.user_name_input.is_visible()
        self.login_button.is_visible()
    
    def enter_user_name(self, username: str):
        self.user_name_input.is_visible()
        self.user_name_input.fill(username)

    def enter_password(self, password: str):
        self.password_input.is_visible()
        self.password_input.fill(password)

    def click_login_button(self):
        self.login_button.is_visible()
        self.login_button.click()