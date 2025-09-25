def payload_create_user(user_name: str, password: str) -> dict:
    return {"userName": user_name, "password": password}


def payload_generate_token(user_name: str, password: str) -> dict:
    return {"userName": user_name, "password": password}


def payload_authorize_user(user_name: str, password: str) -> dict:
    return {"userName": user_name, "password": password}


def payload_add_book(user_id: str) -> dict:
    return {
        "userId": user_id,
        "collectionOfIsbns": [{"isbn": "9781449325862"}],
    }
