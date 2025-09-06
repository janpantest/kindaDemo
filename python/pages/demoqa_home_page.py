from playwright.sync_api import Page, expect
from helpers.book_data import first_run_values
from datetime import datetime

class DemoqaHomePage:

    def __init__(self, page: Page):
        self.page = page
        self.logo_home = page.locator('div#app header a')
        self.input_box = page.locator('input#searchBox')
        self.result = page.locator('span a')
        self.drop_down = page.get_by_label('rows per page')
        self.button_previous = page.get_by_role("button", name="Previous")
        self.button_next = page.get_by_role("button", name="Next")
    
    def go_to(self, url):
        self.page.goto(url)
    
    def check_home_page(self):
        self.logo_home.is_visible()
        self.input_box.is_visible()

    def enter_book(self, book_name):
        self.input_box.fill(book_name)

    def check_rows_amount(self):
        row_count = self.result
        count = row_count.count()
        expected_row = self.result.all()
        print(f"this is count : {count}");
        assert len(expected_row) == 1
        # self.result.count()
        expect(self.result).to_have_text("Git Pocket Guide")

    def get_book_text(self, run: str):
        results = self.result
        # expect(self.result).to_be_visible()
        count = results.count()

        # expected_values = first_run_values if run == "First" else second_run_values

        for i in range(count):
            text = results.nth(i).text_content()
            # assert text == expected_values[i], f"item {i} mismatch: 'expected_values[i]', got '{text}'"
            assert text == first_run_values[i]
            print(text)

            date_str = datetime.now().strftime("%Y-%m-%d")
            content = self.result.all_inner_texts()
            filename = f"output_{date_str}.txt"
    
    # Write to the dynamic file
            with open(filename, "w", encoding="utf-8") as file:
                file.write(f"{content}\n")

            print(f"Items written to {filename}")