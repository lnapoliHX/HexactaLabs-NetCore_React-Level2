import React,{useEffect} from "react";
import PropTypes from "prop-types";
import { Field, reduxForm } from "redux-form";
import { Form, Button } from "reactstrap";
import Validator from "../../../../common/helpers/YupValidator";
import InputField from "../../../../components/inputs/InputField";
import schema from "../validation";
import {fetchAll} from "../../../productType/list/";
import {connect} from "react-redux";





const ProductForm = props => {
  const { handleSubmit, handleCancel,pTypes } = props;
  //console.log(props);
  useEffect(function(){ //save fetchall in global
    props.fetchAll()
  },[])
  
  return (
    <Form onSubmit={handleSubmit}>
      <Field label="Nombre" name="name" component={InputField} type="text" />
      <Field label="Stock" name="stock" component={InputField} type="text" />
      <Field label="Cost Price" name="CostPrice" component={InputField} type="text" />
      <Field label="Sale Price" name="SalePrice" component={InputField} type="text" />
      <div><Field label="Product Type" name="ProductTypeId" component="select"> { pTypes.map((pt) => (<option key={pt.id} value={pt.id}>{pt.description}</option>))}</Field>
      </div>
      <Button className="store-form__button" color="primary" type="submit">
        Guardar
      </Button>
      <Button
        className="store-form__button"
        color="secondary"
        type="button"
        onClick={handleCancel}
      >
        Cancelar
      </Button>
    </Form>
  );
};



ProductForm.propTypes = {
  handleSubmit: PropTypes.func.isRequired,
  handleCancel: PropTypes.func.isRequired,
  pTypes: PropTypes.array.isRequired
};

const mapStateToProps = function(state){
  //console.log(state);
return {
  pTypes:state.productType.list.types
}

}

const ConnectedProductForm = connect(mapStateToProps, {fetchAll})(ProductForm)

export default reduxForm({
  form: "product", 
  validate: Validator(schema),
  enableReinitialize: true
})(ConnectedProductForm);
