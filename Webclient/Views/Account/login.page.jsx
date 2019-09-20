class AccountLoginPage extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      validation_messages: [],
      email: '',
      password: '',
      remember_me: false,
    };

    this.inputChange = this.inputChange.bind(this);
    this.login = this.login.bind(this);
  }

  inputChange(input, value) {
    let state = {};
    state[input] = value;
    this.setState(state);
  }

  login() {
    const { email, password, remember_me } = this.state;
    const data = { email, password, remember_me };
    BOS.UTIL.FETCH
      .post(this.props.urls.api.account_login, data)
      .then((response) => {
        if(!response.action_success) {
          this.setState({
            validation_messages: response.messages
          });
          return;
        }
        window.location.href = this.props.urls.pages.account_index;
      });
  }

  render() {
    const { validation_messages } = this.state;

    return (
      <div id="page-container">
        <div className="row">
          <div className="col-md-4">
            <h4>Login</h4>
            <hr />
            <div className="form-group">
              <label htmlFor="email">Email</label>
              <input id="email" name="email" type="text" className="form-control" 
                value={ this.state.email }
                onChange={ (event) => this.inputChange('email', event.target.value) }
              />
            </div>
            <div className="form-group">
              <label htmlFor="password">Password</label>
              <input id="password" name="password" type="password" className="form-control" 
                value={ this.state.password }
                onChange={ (event) => this.inputChange('password', event.target.value) }
              />
            </div>
            <div className="form-group">
              <div className="checkbox">
                <label htmlFor="remember_me">
                  <input type="checkbox" id="remember_me" name="remember_me"
                    defaultChecked={ this.state.remember_me }
                    onChange={ (event) => this.inputChange('remember_me', event.target.checked) }
                  />
                  &nbsp;Remember Me?
                </label>
              </div>
            </div>

            <ValidationMessages validation_messages={ validation_messages } />

            <div className="form-group">
              <button type="submit" className="btn btn-primary"
                onClick={ this.login }
              >Log in</button>
            </div>
          </div>
          <div className="col-md-6 col-md-offset-2">
          </div>
        </div>
      </div>
    );
  }
}
