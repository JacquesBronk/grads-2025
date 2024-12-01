## May they fight the good figt and survive.

### Repo-Rules
* Always branch off with your tasks with one of these templates. 
    policy: _one liners, under 50-char_ 
    1. doc: update/create/remove-{because}
    2. feat: built this thing
    3. bug: fixed a thing
    4. test: stuff to do with testing
* Comment - Methods with full summary tag. /// purpose? args[]
* have fun, leave comedic comments if you'd like. No excessive swearing(the hard words).


# TO-DO's
* [Bounties](#open-bounties) 
* We need a theme for our drawings, presentations.
* Add more contributors
* Please create bounties for the services

# Infra-proposal

# Tech stack
* We need to choose a no-sql provider
* [Consul](https://github.com/JacquesBronk/configuration-demos/blob/main/Option-2-Consul/Option2.md), it just works.
* Dotnet api's
* [Cloud-Events](https://github.com/cloudevents/sdk-csharp) for Message 

### Questions?
1. What are we using for => lightweight auth provider
2. What is our NoSql technology => mongo|couch-db|other|both?
3. Can we use the megaBool?
```javascript
enum megabool {
   TRUE,
   FALSE,
   NEITHER,
   BOTH,
   MAYBE,
   TRUEISH,
   FALSEISH,
   IT_DEPENDS,
   OSCILLATING,
   ITS_COMPLICATED,
   DOUBLE_TRUE,
   DOUBLE_FALSE,
```   

# Open Bounties
###  1. hellooo-api, service greeter. 
* Hi {username-if logged in}
    * Render Target Ads {robin-hood-traffic}
    * {Promote SingupSpecial IF not logged in} 
* Track session metrics, how long on each page if user has an id. Only entry & exit event unix-epochs. NO-POLLING!



### 2. ads-api, simple server, just gives ads. 

| dev | comment (250 - char)     |
|------------|-------------------|
| ??    | i picked this up on xx-xx-xx, /stuck: here /qa: here /dev: "feat/bug/diag/" |

#### Client facing Endpoints, No auth
```json
{
  "id": "bc94ceaa-9769-486b-8d24-9eab6f9805df",
  "callbackUrl": "http://ads-api/ad-seen/bc94ceaa-9769-486b-8d24-9eab6f9805df",
  "payloadBuilderUpsellUrl": "http://ads-api/{userId}/lu/{unix-epoch}"
}
```

* The __id__ represents the add Id.
* The __callbackUrl__ is used for ad viewed metrics
* The __payloadBuilder__ is a way to render ads for the user specifically, like reccomendations on products etc. The __unix-epoch__ gives us an exact timestamp so we can load adds the user has not seen. Maybe lets look at stuff like hydration strategies over grpc? 

The front end framework?

| Technology | Justification     | Votes |   |   |
|------------|-------------------|-------|---|---|
| Angular    | Everyone knows it |       |   |   |
| Vue        |                   |       |   |   |
| Blazor     |                   |       |   |   |


__[secure]__
#### ads-admin-api, 

Just this data. No more, no less
```json
{
  "session": {
    "id": "8650e977-3578-4ef2-875b-ad6ff88744ac"
  },
  "title": "Half Life 2 50% Off",
  "description": "End's {DateTimeUtc}",
  "image": "http://ads-api/serve-me/{image-id}",
  "button-action": [
    "fn(args[]){ */minified js/htmx */  }" //maybe pre&post event hookups? 
                        //preRender used for canvas-render to target with supplied css
                        //postRender used for event-hookups, metric & reporting success/fail events
                        //
  ],
  "render-css": "<render-target>body { background-colour: #eeeeuw}</render-target>",
  "valid-too": "2014-06-25",
  "id": "bc94ceaa-9769-486b-8d24-9eab6f9805df"
}
```

