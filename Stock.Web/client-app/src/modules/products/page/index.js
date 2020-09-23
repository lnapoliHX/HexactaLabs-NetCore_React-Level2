import "./products.css";
import React, { Component } from "react";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import { Switch, Route } from "react-router-dom";
import List from "../list/container";
import View from "../view/container";
import Create from "../create/container";
import Update from "../update/container";
import Remove from "../remove/container";
import IncreaseStock from "../modifyStock/increaseContainer";
import DecreaseStock from "../modifyStock/decreaseContainer";
import { getLoading, getAll } from "../list";
import Spinner from "../../../components/loading/spinner";

export class Page extends Component {
  componentDidMount() {
    this.props.getAll();
  }

  render() {
    const urls = {
      view: `${this.props.match.url}/view/:id`,
      create: `${this.props.match.url}/create`,
      edit: `${this.props.match.url}/update/:id`,
      remove: `${this.props.match.url}/remove/:id`,
      increaseStock: `${this.props.match.url}/incStock/:id`,
      decreaseStock: `${this.props.match.url}/decStock/:id`
    };

    return (
      <Spinner loading={this.props.loading}>
        <Switch>
          <Route path={urls.view} component={View} />
          <Route path={urls.create} component={Create} />
          <Route path={urls.edit} component={Update} />
          <Route path={urls.increaseStock} component={IncreaseStock} />
          <Route path={urls.decreaseStock} component={DecreaseStock} />
          <Route
            render={() => <List urls={urls} loading={this.props.loading} />}
          />
        </Switch>
        <Route path={urls.remove} component={Remove} />
      </Spinner>
    );
  }
}

Page.propTypes = {
  match: PropTypes.object.isRequired,
  loading: PropTypes.bool.isRequired,
  getAll: PropTypes.func.isRequired
};

const mapStateToProps = state => {
  return { loading: getLoading(state) };
};

const mapDispatchToProps = {
  getAll
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Page);
