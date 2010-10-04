comic "alpha"
start_at "http://example.com/alpha/first_page"

while true:
	download("comic regex")
	if not visit("next page regex"):
		break;