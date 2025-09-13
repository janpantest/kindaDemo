import sys
import os
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))
from dotenv import load_dotenv
from pages.demoqa_home_page import DemoqaHomePage

load_dotenv()

URL = os.getenv("BOOK_URL")

def test_book(page):
    demoqa_home = DemoqaHomePage(page)

    book_name = 'Git'

    print(f"this is url {URL}")

    demoqa_home.go_to(URL)
    demoqa_home.check_home_page()
    demoqa_home.get_book_text("First")
    demoqa_home.enter_book(book_name)
    demoqa_home.check_rows_amount()
        